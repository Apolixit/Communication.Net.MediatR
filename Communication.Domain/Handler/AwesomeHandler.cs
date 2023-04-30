using MediatR;
using Microsoft.Extensions.Logging;
using Communication.Domain.Dto;
using Communication.Domain.Query;
using Communication.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Domain.Handler
{
    public class AwesomeHandler : IRequestHandler<AwesomeQuery, AwesomeResponseDto>
    {
        private readonly IAwesomeRepository _awesomeRepository;
        private readonly ILogger<AwesomeHandler> _logger;

        public AwesomeHandler(IAwesomeRepository awesomeRepository, ILogger<AwesomeHandler> logger)
        {
            _awesomeRepository = awesomeRepository;
            _logger = logger;
        }

        public async Task<AwesomeResponseDto> Handle(AwesomeQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[{request.QueryId}] {nameof(AwesomeQuery)} handle by {nameof(AwesomeHandler)} in {Assembly.GetExecutingAssembly().GetName()}");

            var res = await _awesomeRepository.CallInfrastructureAndMakeSomeNoiseAsync();

            return new AwesomeResponseDto()
            {
                Id = Guid.NewGuid(),
                SourceId = request.QueryId,
                IsSuccess = res
            };
        }
    }
}
