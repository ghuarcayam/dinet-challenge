using Autofac;
using FluentValidation;
using MediatR.Pipeline;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dinet.Module.Challenge.Application.Contract;

namespace Dinet.Module.Challenge.Infraestructure.Configuration.Mediation
{
    internal class MediatRModule:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly)
              .AsImplementedInterfaces()
              .InstancePerLifetimeScope();

            var openHandlerTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(IRequestExceptionHandler<,,>),
                typeof(IRequestExceptionAction<,>),
                typeof(INotificationHandler<>),
                typeof(IValidator<>)
            };

            var assemblyApplication = typeof(IChallengeModule).Assembly;

            foreach (var i in openHandlerTypes)
            {
                builder.RegisterAssemblyTypes(assemblyApplication)
                    .AsClosedTypesOf(i)
                    .AsImplementedInterfaces();
            }

            builder.RegisterType<ServiceProviderWrapper>()
            .As<IServiceProvider>()
            .InstancePerDependency()
            .IfNotRegistered(typeof(IServiceProvider));


        }
    }
}
