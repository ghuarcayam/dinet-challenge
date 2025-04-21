using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dinet.Module.Challenge.Application.Configuration;
using Dinet.Module.Challenge.Domain.Orders;

namespace Dinet.Module.Challenge.Application.Orders.GetOrder
{
    internal class GetOrderQueryHandler : IQueryHandler<GetOrderQuery, OperationResult<GetOrderResult>>
    {
        private readonly IOrderRepository OrderRepository;
 
        public GetOrderQueryHandler(IOrderRepository OrderRepository)
        {
            this.OrderRepository = OrderRepository;
       
        }

        public async Task<OperationResult<GetOrderResult>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var o = await this.OrderRepository.GetAsync(request.OrderId);

            if (o != null) 
            {

                var r = new GetOrderResult()
                {
                    OrderId = o.Id,
                    FechaCreacion = o.FechaCreacion,
                    Cliente = o.Cliente,
                    Items = new List<GetOrderResult.GetOrderResultItem>(),
                    Total = o.Total
                };
                foreach (var item in o.OrderDetails())
                {
                    r.Items.Add(new()
                    {
                        Id = item.Id,
                        Cantidad = item.Cantidad,
                        PrecioUnitario = item.PrecioUnitario,
                        Product = item.Producto,
                        SubTotal = item.SubTotal
                    });
                }

                return OperationResult.Ok(r);
            }
            return OperationResult.Ok(default(GetOrderResult));


        }
    }
}
