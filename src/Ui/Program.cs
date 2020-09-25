using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RESTFulSense.Clients;
using System;
using System.Threading.Tasks;

namespace Ui
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient<IRESTFulApiFactoryClient, RESTFulApiFactoryClient>(client => client.BaseAddress = new Uri("https://localhost:44301/"));
            await builder.Build().RunAsync();
        }
    }
}
