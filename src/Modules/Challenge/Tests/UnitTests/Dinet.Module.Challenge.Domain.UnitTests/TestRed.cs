using Dinet.Module.Challenge.Domain.Orders;

namespace Dinet.Module.Challenge.UnitTests
{
    public class TestsRed
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestStatusOrderMustBeActiveFail()
        {
            var order = new Order(DateTime.Today, "Client");

            var item1 = order.AddItem("product", 10, 10);

            var item2 = order.AddItem("product2", 11, 120);

            Assert.That(order.Total, Is.Not.EqualTo(190));

            Assert.That(order.Total, Is.EqualTo((10*10)+(11*120)));

        }

       
    }
}