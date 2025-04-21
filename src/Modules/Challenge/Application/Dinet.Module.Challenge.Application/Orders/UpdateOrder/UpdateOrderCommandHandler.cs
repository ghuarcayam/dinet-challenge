using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dinet.Module.Challenge.Application.Configuration;
using Dinet.Module.Challenge.Domain.Orders;

namespace Dinet.Module.Challenge.Application.Orders.UpdateOrder
{
    internal class UpdateOrderCommandHandler : ICommandHandler<UpdateOrderCommand, OperationResult>
    {
        private readonly IOrderRepository OrderRepository;
        public UpdateOrderCommandHandler(IOrderRepository OrderRepository) 
        {
            this.OrderRepository = OrderRepository;
        }
        public async Task<OperationResult> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            if (!await OrderRepository.DetectDuplicates(request.Id, request.Cliente, request.FechaCreacion)) 
            {
                var o = await this.OrderRepository.GetAsync(request.Id);
                if (o != null) 
                {
                    o.Modify(o.FechaCreacion, request.Cliente);

                    foreach (var deletedItem in o.OrderDetails().Where(x => !request.OrdenDetails.Any(y => y.Id == x.Id)).ToList())
                    {
                        o.RemoveById(deletedItem.Id);
                    }

                    foreach (var item in request.OrdenDetails)
                    {
                        if (item.Id.HasValue)
                            o.ModifyItem(item.Id.Value, item.Producto, item.Cantidad, item.PrecioUnitario);
                        else
                            o.AddItem(item.Producto, item.Cantidad, item.PrecioUnitario);
                    }

                    o.EnsureHasItems();

                    await this.OrderRepository.UpdateAsync(o, cancellationToken);
                }

                return OperationResult.Ok();
            }
            return OperationResult.WithError<Guid>("Duplication cliente & fecha creacion");




        }
    }
}
