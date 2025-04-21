using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dinet.Module.Challenge.Application;

namespace Dinet.Module.Challenge.IntegrationTests
{
    public class ExecutionContextMock : IExecutionContextAccessor
    {
        public Guid CorrelationId { get; }

        public bool IsAvailable { get; }
    }
}
