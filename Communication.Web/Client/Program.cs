using MediatR.Courier;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Communication.Domain.Handler;
using Communication.Domain.Repository;
using Communication.Web;

namespace Communication.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(AwesomeHandler).Assembly);
            })
            .AddCourier(typeof(AwesomeHandler).Assembly)
            .AddTransient<IAwesomeRepository, AwesomeRepository>();

            await builder.Build().RunAsync();
        }
    }
}