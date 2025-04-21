using Autofac;
using Serilog;
using Dinet.Module.Challenge.Application;
using Dinet.Module.Challenge.Infraestructure.Configuration.Caching;
using Dinet.Module.Challenge.Infraestructure.Configuration.DataAccess;
using Dinet.Module.Challenge.Infraestructure.Configuration.Logging;
using Dinet.Module.Challenge.Infraestructure.Configuration.Mediation;
using Dinet.Module.Challenge.Infraestructure.Configuration.Processing;

namespace Dinet.Module.Challenge.Infraestructure.Configuration
{
    public static class ChallengeStartup
    {
        public static void Start(ILogger logger, IExecutionContextAccessor executionContextAccessor) 
        {
            ConfigureCompositionRoot(logger, executionContextAccessor);
        }
        private static void ConfigureCompositionRoot(ILogger logger, IExecutionContextAccessor executionContextAccessor) 
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new LoggingModule(logger));

            containerBuilder.RegisterModule(new MediatRModule());

            containerBuilder.RegisterModule(new ProcessingModule());

            containerBuilder.RegisterModule(new DataAccessModule());

            containerBuilder.RegisterModule(new CachingModule());

            containerBuilder.RegisterInstance(executionContextAccessor);

            var container = containerBuilder.Build();

            ChallengeCompositionRoot.SetContainer(container);
        }
    }
}
