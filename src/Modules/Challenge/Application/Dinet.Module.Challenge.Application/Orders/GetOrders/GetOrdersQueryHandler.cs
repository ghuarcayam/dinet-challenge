using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dinet.Module.Challenge.Application.Configuration;
using Dinet.Module.Challenge.Domain.Orders;

namespace Dinet.Module.Challenge.Application.Orders.GetOrders
{
    internal class GetOrdersQueryHandler : IQueryHandler<GetOrdersQuery, OperationResult<GetOrdersResult>>
    {
        private readonly IOrderRepository orderRepository;

        public GetOrdersQueryHandler(IOrderRepository OrderRepository)
        {
            this.orderRepository = OrderRepository;

        }

        public async Task<OperationResult<GetOrdersResult>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var query = await orderRepository.GetAsync(request.StartAt, request.Size, request.Cliente, request.From, request.To, request.FieldOrder, request.OrderDesc, cancellationToken);

            var items= query.Item1.Select(x => new GetOrdersResult.PaginationOrder() { Cliente=x.Cliente, FechaCreacion=x.FechaCreacion, Id=x.Id, Total= x.Total, Items= x.OrderDetails().Select(y=> new GetOrdersResult.PaginationOrderItem() { Cantidad=y.Cantidad, Id=y.Id, PrecioUnitario=y.PrecioUnitario, Producto=y.Producto, SubTotal=y.SubTotal  }) });



            return OperationResult.Ok(new GetOrdersResult() { TotalRows = query.Item2, Orders = items });

        }
    }
}
