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
using Microsoft.Identity.Client;

using Prometheus;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization();

var redis = ConnectionMultiplexer.Connect("192.168.1.174:6379"); // Replace with actual Redis server IP 88
builder.Services.AddDataProtection()
    .PersistKeysToStackExchangeRedis(redis, "DataProtection-Keys")
    .SetApplicationName("GotorzApp");

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redis.Configuration;
    options.InstanceName = "GotorzApp"; // optional namespace prefix
});

builder.Services.AddSignalR();
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
builder.Services.AddScoped<IPhotoService, PhotoService>();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

// Add database context
builder.Services.AddDbContextFactory<GotorzContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Docker"),
        b => b.MigrationsAssembly("SharedLib")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<GotorzAppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<GotorzContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<GotorzAppUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

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