using Autofac;
using Dinet.Module.Challenge.Application.Contract;
using Dinet.Module.Challenge.Infraestructure;

namespace Challenge.API.Modules.Challenge
{
    public class ChallengeAutofacModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ChallengeModule>().As<IChallengeModule>().InstancePerLifetimeScope();
        }
    }
}
