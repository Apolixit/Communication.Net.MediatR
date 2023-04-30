using MediatR;
using Microsoft.Extensions.Logging;
using Communication.Domain.Notification;

namespace Communication.Domain.Repository
{
    public class AwesomeRepository : IAwesomeRepository
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AwesomeRepository> _logger;

        public AwesomeRepository(IMediator mediator, ILogger<AwesomeRepository> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<bool> CallInfrastructureAndMakeSomeNoiseAsync()
        {
            // We can do some database access here or custom business logic...
            Thread.Sleep(1000);

            _logger.LogDebug($"Publish an event from {nameof(AwesomeRepository)}");
            await _mediator.Publish(new SomethingHappenedNotification()
            {
                Message = $"[{Guid.NewGuid()}] I'm a notification from {nameof(AwesomeRepository)}"
            });

            return true;
        }
    }
}
