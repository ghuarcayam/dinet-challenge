using NetArchTest.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinet.Module.Challenge.ArchTests
{
    [TestFixture]
    public class LayersTests
    {
        [Test]
        public void DomainLayer_DoesNotHaveDependency_ToApplicationLayer()
        {
            var result = Types.InAssembly(TestHelper.DomainAssembly)
                .Should()
                .NotHaveDependencyOn(TestHelper.ApplicationAssembly.GetName().Name)
                .GetResult();

            TestHelper.AssertArchTestResult(result);
        }

        [Test]
        public void DomainLayer_DoesNotHaveDependency_ToInfrastructureLayer()
        {
            var result = Types.InAssembly(TestHelper.DomainAssembly)
                .Should()
                .NotHaveDependencyOn(TestHelper.ApplicationAssembly.GetName().Name)
                .GetResult();

            TestHelper.AssertArchTestResult(result);
        }

        [Test]
        public void ApplicationLayer_DoesNotHaveDependency_ToInfrastructureLayer()
        {
            var result = Types.InAssembly(TestHelper.ApplicationAssembly)
                .Should()
                .NotHaveDependencyOn(TestHelper.InfrastructureAssembly.GetName().Name)
                .GetResult();

            TestHelper.AssertArchTestResult(result);
        }
    }
}
