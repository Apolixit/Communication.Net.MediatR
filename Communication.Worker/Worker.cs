using MediatR;
using MediatR.Courier;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Communication.Domain.Notification;
using Communication.Domain.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Worker
{
    /// <summary>
    /// Demo background worker
    /// </summary>
    public class Worker : BackgroundService
    {
        private readonly IMediator _mediator;
        private readonly ICourier _courier;
        private readonly ILogger<Worker> _logger;

        public Worker(IMediator mediator, ICourier courier, ILogger<Worker> logger)
        {
            _mediator = mediator;
            _courier = courier;
            _logger = logger;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            bool execute = true;
            var periodicTimer = new PeriodicTimer(TimeSpan.FromSeconds(100));

            _courier.SubscribeWeak<SomethingHappenedNotification>(SomethingHappenedNotificationWithCourier);
            _courier.SubscribeWeak<BackgroundWorkerNotification>(BackgroundWorkerNotificationWithCourier);

            while (!stoppingToken.IsCancellationRequested) {
                if(execute)
                {
                    await DoSomethingAsync();

                    var query = new AwesomeQuery()
                    {
                        QueryId = Random.Shared.Next(0, 1000)
                    };

                    _logger.LogDebug($"[{query.QueryId}] Prepare call MediatR");

                    var res = await _mediator.Send(query, CancellationToken.None);

                    _logger.LogDebug($"[{res.SourceId}] MediatR response received : RequestId = {res.Id} / Success = {res.IsSuccess}");
                }

                execute = await periodicTimer.WaitForNextTickAsync();
            }
        }

        public async Task DoSomethingAsync()
        {
            await Task.Run(() =>
            {
                int i = 0;

                // Do something
            });
        }

        public async Task SomethingHappenedNotificationWithCourier(SomethingHappenedNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{notification.Message} console app Courier");

            // And now throw a new notification (caught by console app only for the demo)
            await _mediator.Publish(new BackgroundWorkerNotification() { AnotherMessage = "Message from background worker" });
        }

        public Task BackgroundWorkerNotificationWithCourier(BackgroundWorkerNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{notification.AnotherMessage} console app Courier");

            return Task.CompletedTask;
        }
    }
}
