using Dinet.Module.Challenge.Application.Orders.CreateOrder;
using Dinet.Module.Challenge.Application.Orders.DeleteOrder;
using Dinet.Module.Challenge.Application.Orders.GetOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinet.Module.Challenge.IntegrationTests
{
    public class OrderDeleteTests: TestBase
    {
        [Test]
        public async Task Delete_Order_Happy_Path_Test() 
        {
            var cmd = new CreateOrderCommand($"{OrderSampleData.ClientName} for deleted", DateTime.Today);

            cmd.OrdenDetails.Add(new(OrderSampleData.ProductName, 10, 12));

            cmd.OrdenDetails.Add(new(OrderSampleData.ProductName2, 10, 2));

            var result = await ChallengeModule.ExecuteCommandAsync(cmd);

            Assert.IsTrue(result.Success);

            Assert.That(Guid.Empty, Is.Not.EqualTo(result.Data));

            var cmdDelete = new DeleteOrderCommand(result.Data);

            var resultDelete = await ChallengeModule.ExecuteCommandAsync(cmdDelete);

            Assert.IsTrue(resultDelete.Success);

            var cmdGetQuery = new GetOrderQuery(result.Data);

            var resulQuery = await ChallengeModule.ExecuteQueryAsync(cmdGetQuery);

            Assert.IsTrue(resulQuery.Success);
        }
    }
}
