using Dinet.Module.Challenge.Application.Orders.CreateOrder;
using Dinet.Module.Challenge.Application.Orders.GetOrder;
using Dinet.Module.Challenge.Application.Orders.UpdateOrder;
using Dinet.Module.Challenge.Infraestructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinet.Module.Challenge.IntegrationTests
{
    public class OrderUpdateTests: TestBase
    {
        [Test]
        public async Task Update_Order_Happy_Path_Test() 
        {
            var cmd = new CreateOrderCommand($"{ OrderSampleData.ClientName } for update", DateTime.Today);

            cmd.OrdenDetails.Add(new(OrderSampleData.ProductName, 10, 12));

            cmd.OrdenDetails.Add(new(OrderSampleData.ProductName2, 10, 2));

            var result = await ChallengeModule.ExecuteCommandAsync(cmd);

            Assert.IsTrue(result.Success);

            Assert.That(Guid.Empty, Is.Not.EqualTo(result.Data));

            var cmdUpdate = new UpdateOrderCommand(result.Data, $"{OrderSampleData.ClientName} updated", DateTime.Today);

            cmdUpdate.OrdenDetails.Add(new(null, $"{OrderSampleData.ProductName} updated", 11, 13));

            var resultUpdate = await ChallengeModule.ExecuteCommandAsync(cmdUpdate);

            Assert.IsTrue(resultUpdate.Success);

            var cmdGetQuery = new GetOrderQuery(result.Data);

            var resulQuery = await ChallengeModule.ExecuteQueryAsync(cmdGetQuery);

            Assert.IsTrue(resulQuery.Success);

            decimal totalExpected = (11 * 13);

            Assert.That(resulQuery.Data.Total, Is.EqualTo(totalExpected));

            Assert.IsTrue(resulQuery.Data.Items.Count == 1);
        }
    }
}
