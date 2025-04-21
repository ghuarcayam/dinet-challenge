using FluentValidation;
using NetArchTest.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dinet.Module.Challenge.Application.Configuration;
using Dinet.Module.Challenge.Application.Contract;

namespace Dinet.Module.Challenge.ArchTests
{
    [TestFixture]
    public class ApplicationLayerTests
    {
        [Test]
        public void QueryShouldBeImmutable()
        {
            var types = Types.InAssembly(TestHelper.ApplicationAssembly)
                .That().ImplementInterface(typeof(IQuery<>)).GetTypes();

            TestHelper.AssertAreImmutable(types);
        }
        [Test]
        public void CommandHandler_Should_Have_Name_EndingWith_CommandHandler()
        {
            var result = Types.InAssembly(TestHelper.ApplicationAssembly)
                .That()
                .ImplementInterface(typeof(ICommandHandler<,>))
                .And()
                .DoNotHaveNameMatching(".*Decorator.*").Should()
                .HaveNameEndingWith("CommandHandler")
                .GetResult();

            TestHelper.AssertArchTestResult(result);
        }

        [Test]
        public void QueryHandler_Should_Have_Name_EndingWith_QueryHandler()
        {
            var result = Types.InAssembly(TestHelper.ApplicationAssembly)
                .That()
                .ImplementInterface(typeof(IQueryHandler<,>))
                .Should()
                .HaveNameEndingWith("QueryHandler")
                .GetResult();

            TestHelper.AssertArchTestResult(result);
        }

        [Test]
        public void Command_And_Query_Handlers_Should_Not_Be_Public()
        {
            var types = Types.InAssembly(TestHelper.ApplicationAssembly)
                .That()
                    .ImplementInterface(typeof(IQueryHandler<,>))
                        .Or()
                    .ImplementInterface(typeof(ICommandHandler<,>))
                .Should().NotBePublic().GetResult().FailingTypes;

            TestHelper.AssertFailingTypes(types);
        }

        [Test]
        public void Validator_Should_Have_Name_EndingWith_Validator()
        {
            var result = Types.InAssembly(TestHelper.ApplicationAssembly)
                .That()
                .Inherit(typeof(AbstractValidator<>))
                .Should()
                .HaveNameEndingWith("Validator")
                .GetResult();

            TestHelper.AssertArchTestResult(result);
        }

        [Test]
        public void Validators_Should_Not_Be_Public()
        {
            var types = Types.InAssembly(TestHelper.ApplicationAssembly)
                .That()
                .Inherit(typeof(AbstractValidator<>))
                .Should().NotBePublic().GetResult().FailingTypes;

            TestHelper.AssertFailingTypes(types);
        }

    }
}
