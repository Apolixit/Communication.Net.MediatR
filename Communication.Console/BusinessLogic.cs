using MediatR;
using MediatR.Courier;
using Microsoft.Extensions.Logging;
using Communication.Domain.Notification;
using Communication.Domain.Query;

namespace Communication.Console
{
    public class BusinessLogic
    {
        private readonly IMediator _mediator;
        private readonly ICourier _courier;
        private readonly ILogger<BusinessLogic> _logger;

        public BusinessLogic(IMediator mediator, ICourier courier, ILogger<BusinessLogic> logger)
        {
            _mediator = mediator;
            _courier = courier;
            _logger = logger;
        }

        /// <summary>
        /// Program default entry point
        /// </summary>
        /// <returns></returns>
        public async Task StartAsync()
        {
            _logger.LogDebug("Console project start");

            var periodicTimer = new PeriodicTimer(TimeSpan.FromSeconds(100));

            _courier.SubscribeWeak<SomethingHappenedNotification>(SomethingHappenedNotificationWithCourier);

            do
            {
                var query = new AwesomeQuery()
                {
                    QueryId = Random.Shared.Next(0, 1000)
                };

                _logger.LogDebug($"[{query.QueryId}] Prepare call MediatR");

                var res = await _mediator.Send(query, CancellationToken.None);

                _logger.LogDebug($"[{res.SourceId}] MediatR response received : RequestId = {res.Id} / Success = {res.IsSuccess}");
            } while(await periodicTimer.WaitForNextTickAsync());
        }

        public void SomethingHappenedNotificationWithCourier(SomethingHappenedNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{notification.Message} console app Courier");
        }
    }
}
