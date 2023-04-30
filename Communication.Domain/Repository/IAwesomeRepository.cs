using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Domain.Repository
{
    /// <summary>
    /// Fake some database / blockchain / cloud repository
    /// </summary>
    public interface IAwesomeRepository
    {
        Task<bool> CallInfrastructureAndMakeSomeNoiseAsync();
    }
}
