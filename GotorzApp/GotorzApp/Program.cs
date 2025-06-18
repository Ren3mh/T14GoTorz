using GotorzApp.Components;
using GotorzApp.Components.Account;
using SharedLib.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedLib;
using StackExchange.Redis;
using Microsoft.AspNetCore.DataProtection;
using SharedLib.Service;
using GotorzApp.Hubs;
using Microsoft.AspNetCore.ResponseCompression;

using Prometheus;
using GotorzApp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Employee", policy =>
          policy.RequireRole("Admin", "Guide"));
});

builder.Services.AddBlazorBootstrap();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization();

var configuration = builder.Configuration;
var redisConnString = configuration.GetConnectionString("redisDb");
Console.WriteLine(redisConnString);

var mssqlConnString = configuration.GetConnectionString("mssqlDb");
Console.WriteLine(mssqlConnString);

var redis = ConnectionMultiplexer.Connect(redisConnString);
builder.Services.AddDataProtection()
    .PersistKeysToStackExchangeRedis(redis, "DataProtection-Keys")
    .SetApplicationName("GotorzApp");

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redis.Configuration;
    options.InstanceName = "GotorzApp"; // optional namespace prefix
});

builder.Services.AddSignalR()
    .AddStackExchangeRedis(redisConnString, options =>
    {
        options.Configuration.ChannelPrefix = "GotorzApp"; // optional channel prefix
    });
builder.Services.AddResponseCompression(options =>
{
    //options.EnableForHttps = true;
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

builder.Services.AddHttpClient<CurrentWeatherService>();
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<ITravelPackageService, TravelPackageService>();
builder.Services.AddScoped<IService<Flightpath>, FlightpathService>();
builder.Services.AddScoped<IIataLocationService, IataLocationService>();
builder.Services.AddScoped<ICurrentWeatherService, CurrentWeatherService>();
builder.Services.AddScoped<ChatService>();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

// Add database context
builder.Services.AddDbContextFactory<GotorzContext>(options =>
    options.UseSqlServer(
        mssqlConnString,
        b => b.MigrationsAssembly("SharedLib")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<GotorzAppUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
    .AddEntityFrameworkStores<GotorzContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<GotorzAppUser>, IdentityNoOpEmailSender>();

// Add the initialization service
// builder.Services.AddScoped<InitializeFakeUsers>();

var app = builder.Build();

// Add fake users and roles
//using (var scope = app.Services.CreateScope())
//{
//    var initializationService = scope.ServiceProvider.GetRequiredService<InitializeFakeUsers>();
//    await initializationService.InitializeAsync();
//}

// This adds the /metrics endpoint
app.UseMetricServer();

// Optional: Middleware to count visitors (for example, each HTTP request)
var counter = Prometheus.Metrics.CreateCounter("visitor_count", "Number of visitors");

app.Use(async (context, next) =>
{
    if (context.Request.Path != "/metrics")
    {
        counter.Inc();
    }
    await next();
});

app.UseResponseCompression();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(GotorzApp.Client._Imports).Assembly);



// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.MapHub<ChatHub>("/chathub");

app.Run();