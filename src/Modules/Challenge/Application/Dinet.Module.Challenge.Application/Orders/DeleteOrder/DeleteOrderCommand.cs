using Dinet.Module.Challenge.Application.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinet.Module.Challenge.Application.Orders.DeleteOrder
{
    public class DeleteOrderCommand: ICommand<OperationResult>
    {
        public DeleteOrderCommand(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; }
    }
}
