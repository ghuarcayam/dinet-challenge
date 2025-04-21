using Autofac;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinet.Module.Challenge.Infraestructure.Configuration.DataAccess
{
    internal class DataAccessModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder) 
        {

            builder
               .Register(c =>
               {
                   var dbContextOptionsBuilder = new DbContextOptionsBuilder<ChallengeContext>();
                   dbContextOptionsBuilder.UseInMemoryDatabase("DbChallengeDinet");

                   /*dbContextOptionsBuilder
                       .ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();
                   */

                   return new ChallengeContext(dbContextOptionsBuilder.Options);
               })
               .AsSelf()
               .As<DbContext>()
               .SingleInstance();

            var infrastructureAssembly = typeof(ChallengeContext).Assembly;

            builder.RegisterAssemblyTypes(infrastructureAssembly)
                .Where(type => type.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
