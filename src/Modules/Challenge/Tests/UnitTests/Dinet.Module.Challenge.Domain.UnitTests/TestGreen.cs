

using Dinet.Module.Challenge.Domain;
using Dinet.Module.Challenge.Domain.Orders;

namespace Dinet.Module.Challenge.UnitTests
{
    public class TestGreen
    {
        [Test]
        public void TestOrderWithoutDetails() 
        {
            var order = new Order(DateTime.Today, "Client");

            Assert.Catch<BusinessRuleValidationException>(()=> order.EnsureHasItems());
         
        }

        [Test]
        public void TestPriceAndCountNegativeCatch()
        {
            var order = new Order(DateTime.Today, "Client");

            Assert.Catch<BusinessRuleValidationException>(()=> order.AddItem("product", -10, -10));
        }
    }
}
