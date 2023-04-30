using MediatR;
using Microsoft.Extensions.Logging;
using Communication.Domain.Notification;

namespace Communication.Domain.Handler
{
    public class SomethingHappenedNotificationDomainHandler : INotificationHandler<SomethingHappenedNotification>
    {
        private readonly ILogger<SomethingHappenedNotificationDomainHandler> _logger;

        public SomethingHappenedNotificationDomainHandler(ILogger<SomethingHappenedNotificationDomainHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(SomethingHappenedNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Domain application notification received !");

            return Task.CompletedTask;
        }
    }
}
