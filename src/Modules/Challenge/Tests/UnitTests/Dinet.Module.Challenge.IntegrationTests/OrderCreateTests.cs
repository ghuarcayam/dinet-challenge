using Dinet.Module.Challenge.Application.Orders.CreateOrder;
using Dinet.Module.Challenge.Application.Orders.GetOrder;
using Dinet.Module.Challenge.Infraestructure.Configuration.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinet.Module.Challenge.IntegrationTests
{
    public class OrderCreateTests : TestBase
    {
        [Test]
        public async Task Create_Order_Happy_Path_Test() 
        {
            var cmd = new CreateOrderCommand(OrderSampleData.ClientName, DateTime.Today);

            cmd.OrdenDetails.Add(new(OrderSampleData.ProductName, 10, 12 ));

            cmd.OrdenDetails.Add(new(OrderSampleData.ProductName2, 10, 2));

            var result = await ChallengeModule.ExecuteCommandAsync(cmd);

            Assert.IsTrue(result.Success);

            Assert.That(Guid.Empty, Is.Not.EqualTo(result.Data));

            var cmdGetQuery = new GetOrderQuery(result.Data);

            var resulQuery = await ChallengeModule.ExecuteQueryAsync(cmdGetQuery);

            Assert.IsTrue(resulQuery.Success);

            decimal totalExpected = (10 * 12) + (10 * 2);

            Assert.That(resulQuery.Data.Total, Is.EqualTo(totalExpected));
            
            Assert.IsTrue(
                resulQuery.Data.Items.Any(x => x.Product == OrderSampleData.ProductName && x.SubTotal == 10 * 12)
            );

            Assert.IsTrue(
                resulQuery.Data.Items.Any(x => x.Product == OrderSampleData.ProductName2 && x.SubTotal == 10 * 2)
            );
        }

        [Test]
        public async Task Create_Order_Without_Details_Test() 
        {
            var cmd = new CreateOrderCommand($"{OrderSampleData.ClientName} without details", DateTime.Today);

            Assert.ThrowsAsync<InvalidCommandException>(async () => await ChallengeModule.ExecuteCommandAsync(cmd));
        }

        [Test]
        public async Task Create_Order_With_Values_Negatives_test() 
        {
            var cmd = new CreateOrderCommand($"{OrderSampleData.ClientName} Negative values", DateTime.Today);

            cmd.OrdenDetails.Add(new(OrderSampleData.ProductName, -1, 12));

            cmd.OrdenDetails.Add(new(OrderSampleData.ProductName2, 10, 0));

            Assert.ThrowsAsync<InvalidCommandException>(async () => await ChallengeModule.ExecuteCommandAsync(cmd));
        }
        [Test]
        public async Task Create_Order_With_Duplication_test()
        {
            var cmd = new CreateOrderCommand($"{OrderSampleData.ClientName} duplicado", DateTime.Today);

            cmd.OrdenDetails.Add(new(OrderSampleData.ProductName, 1, 12));

            cmd.OrdenDetails.Add(new(OrderSampleData.ProductName2, 10, 1));

            var result1 = await ChallengeModule.ExecuteCommandAsync(cmd);

            Assert.IsTrue(result1.Success);

            var result2 = await ChallengeModule.ExecuteCommandAsync(cmd);

            Assert.IsFalse(result2.Success);

        }
    }
}
