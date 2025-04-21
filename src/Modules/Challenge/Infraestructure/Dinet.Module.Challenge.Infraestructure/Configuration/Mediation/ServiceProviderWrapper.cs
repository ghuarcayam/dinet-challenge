using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinet.Module.Challenge.Infraestructure.Configuration.Mediation
{
    internal class ServiceProviderWrapper : IServiceProvider
    {
        private readonly ILifetimeScope lifeTimeScope;

        public ServiceProviderWrapper(ILifetimeScope lifeTimeScope)
        {
            this.lifeTimeScope = lifeTimeScope;
        }

        public object? GetService(Type serviceType) => this.lifeTimeScope.ResolveOptional(serviceType);
    }
}
