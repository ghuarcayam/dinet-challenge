using NSubstitute;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using Dinet.Module.Challenge.Application.Contract;
using Dinet.Module.Challenge.Infraestructure;
using Dinet.Module.Challenge.Infraestructure.Configuration;


namespace Dinet.Module.Challenge.IntegrationTests
{
    public abstract class TestBase
    {
        protected Serilog.ILogger Logger { get; private set; }

        protected IChallengeModule ChallengeModule { get; private set; }

        protected string UrlBase { get; private set; }
        protected ExecutionContextMock ExecutionContext { get; private set; }

        [SetUp]
        public async Task BeforeEachTest() 
        {
            UrlBase = "https://65bbd17252189914b5bd245b.mockapi.io/api/v1";
            ExecutionContext = new ExecutionContextMock();
            Logger = Substitute.For<Serilog.ILogger>();
            ChallengeStartup.Start(UrlBase, Logger, ExecutionContext);

            ChallengeModule = new ChallengeModule();
        }
    }
}
