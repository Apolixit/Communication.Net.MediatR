using MediatR.Courier;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Communication.Domain.Handler;
using Communication.Domain.Notification;
using Communication.Domain.Repository;
using Communication.Worker;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(AwesomeHandler).Assembly);
        })
        .AddCourier(typeof(AwesomeHandler).Assembly) // Listen event from Domain
        .AddLogging(l =>
        {
            l.ClearProviders();
            l.AddConsole();
            l.SetMinimumLevel(LogLevel.Information);
        })
        .AddTransient<IAwesomeRepository, AwesomeRepository>();
    })
    .Build();

await host.RunAsync();