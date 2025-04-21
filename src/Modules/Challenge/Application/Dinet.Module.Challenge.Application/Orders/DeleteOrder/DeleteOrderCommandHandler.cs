using Dinet.Module.Challenge.Application.Configuration;
using Dinet.Module.Challenge.Application.Orders.CreateOrder;
using Dinet.Module.Challenge.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinet.Module.Challenge.Application.Orders.DeleteOrder
{
    internal class DeleteOrderCommandHandler : ICommandHandler<DeleteOrderCommand, OperationResult>
    {
        private readonly IOrderRepository orderRepository;
        public DeleteOrderCommandHandler(IOrderRepository orderRepository) 
        {
            this.orderRepository = orderRepository;
        }
        public async Task<OperationResult> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            await this.orderRepository.DeleteAsync(request.OrderId, cancellationToken);

            return OperationResult.Ok();
        }
    }
}
