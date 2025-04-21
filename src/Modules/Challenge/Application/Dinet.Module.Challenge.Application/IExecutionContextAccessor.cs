using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinet.Module.Challenge.Application
{
    public interface IExecutionContextAccessor
    {
         Guid CorrelationId { get; }
        bool IsAvailable { get; }
    }
}
