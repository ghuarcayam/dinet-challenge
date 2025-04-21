using Dinet.Module.Challenge.Application.Orders.CreateOrder;
using Dinet.Module.Challenge.Application.Orders.GetOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinet.Module.Challenge.IntegrationTests
{
    public class OrderQueryTests: TestBase
    {
        [Test]
        public async Task Pagination_Happy_Path_Test() 
        {
            for (int i = 0; i < 100; i++)
            {
                var cmd = new CreateOrderCommand($"{OrderSampleData.ClientName} {i}", DateTime.Today.AddDays(i));

                cmd.OrdenDetails.Add(new(OrderSampleData.ProductName, 10, 12));

                cmd.OrdenDetails.Add(new(OrderSampleData.ProductName2, 10, 2));

                var result = await ChallengeModule.ExecuteCommandAsync(cmd);

                Assert.IsTrue(result.Success);

                Assert.That(Guid.Empty, Is.Not.EqualTo(result.Data));
            }

            var cmdPag = new GetOrdersQuery(0, 10, null, null, null, null, true);

            var resultPagination = await ChallengeModule.ExecuteQueryAsync(cmdPag);

            Assert.IsTrue(resultPagination.Success);
        }
    }
}
