using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Domain.Dto
{
    public class AwesomeResponseDto
    {
        public Guid Id { get; set; }
        public int SourceId { get; set; }
        public bool IsSuccess { get; set; }
    }
}
