using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dinet.Module.Challenge.Application.Configuration;
using Dinet.Module.Challenge.Domain.Orders;

namespace Dinet.Module.Challenge.Application.Orders.CreateOrder
{
    internal class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, OperationResult<Guid>>
    {
        private readonly IOrderRepository OrderRepository;
        public CreateOrderCommandHandler(IOrderRepository OrderRepository) 
        {
            this.OrderRepository = OrderRepository; 
        }
        public async Task<OperationResult<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            if (!await OrderRepository.DetectDuplicates(default, request.Cliente, request.FechaCreacion)) 
            {
            
                var o = new Order(request.FechaCreacion, request.Cliente);

                foreach (var item in request.OrdenDetails)
                {
                    o.AddItem(item.Producto, item.Cantidad, item.PrecioUnitario);
                }

                o.EnsureHasItems();

                await this.OrderRepository.AddAsync(o);

                return OperationResult.Ok(o.Id);
            }
            return OperationResult.WithError<Guid>("Duplication cliente & fecha creacion");
        }
    }
}
