using MediatR.Courier;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Communication.Domain.Handler;
using Communication.Domain.Notification;
using Communication.Domain.Repository;
using System.Reflection;

namespace Communication.Console
{
    internal class Program
    {
        async static Task Main(string[] _)
        {
            var services = new ServiceCollection();
            var serviceProvider = services.AddLogging(l =>
            {
                l.ClearProviders();
                l.AddConsole();
                l.SetMinimumLevel(LogLevel.Information);
            })
            .AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(AwesomeHandler).Assembly);
            })
            .AddCourier(
                typeof(AwesomeHandler).Assembly, // Listen event from Domain
                typeof(BackgroundWorkerNotification).Assembly) // Listen event from background worker
            .AddSingleton<BusinessLogic>()
            .AddTransient<IAwesomeRepository, AwesomeRepository>()
            .BuildServiceProvider();
            
            await serviceProvider.GetService<BusinessLogic>().StartAsync();
        }
    }
}