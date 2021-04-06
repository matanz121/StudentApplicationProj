using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using StudentsApplicationProj.Client.Services;
using Blazored.LocalStorage;

namespace StudentsApplicationProj.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services
                .AddScoped<IClientAuthService, ClientAuthService>()
                .AddScoped<IHttpService, HttpService>();
            
            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddMudServices();
            
            var host = builder.Build();
           
            var authenticationService = host.Services.GetRequiredService<IClientAuthService>();
            await authenticationService.Initialize();

            await host.RunAsync();
        }
    }
}
