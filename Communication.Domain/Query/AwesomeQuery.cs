using MediatR;
using Communication.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Domain.Query
{
    /// <summary>
    /// Simple test query
    /// </summary>
    public class AwesomeQuery : IRequest<AwesomeResponseDto>
    {
        public int QueryId { get; set; }
    }
}
