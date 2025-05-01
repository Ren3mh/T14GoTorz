//using GotorzApp.Client.Pages;
using System;
using GotorzApp.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared;
using Shared.Data;
using Shared.Service;

namespace GotorzApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();

            builder.Services.AddBlazorBootstrap();

            //Add database context
            builder.Services.AddDbContextFactory<GotorzContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("LocalString")));
            builder.Services.AddHttpClient<CurrentWeatherService>();

            builder.Services.AddScoped<IFlightService, FlightService>();
            builder.Services.AddScoped<IService<Hotel>, HotelService>();
            builder.Services.AddScoped<IService<TravelPackage>, TravelPackageService>();
            builder.Services.AddScoped<IService<Flightpath>, FlightpathService>();
            builder.Services.AddScoped<IService<IataLocation>, IataLocationService>();
            builder.Services.AddScoped<CurrentWeatherService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

            app.Run();
        }
    }
}
