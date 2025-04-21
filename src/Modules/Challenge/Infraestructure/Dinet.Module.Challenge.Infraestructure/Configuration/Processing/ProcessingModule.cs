using Autofac;
using MediatR.Pipeline;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dinet.Module.Challenge.Application.Contract;

namespace Dinet.Module.Challenge.Infraestructure.Configuration.Processing
{
    internal class ProcessingModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            //builder.RegisterType<DomainEventsDispatcher>().As<IDomainEventsDispatcher>().InstancePerLifetimeScope();
            //builder.RegisterType<DomainEventsAccessorFromHubEntity>().As<IDomainEventsAccessor>().InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(LoggingCommandPipeBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(ValidationCommandPipeBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(UnitOfWorkCommandPipeBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestExceptionActionProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestExceptionProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));

            var assemblyApplication = typeof(IChallengeModule).Assembly;
            //builder.RegisterAssemblyTypes(assemblyApplication)
            //   .AsClosedTypesOf(typeof(IDomainEventNotification<>))
            //   .InstancePerDependency();
        }
    }
}
