using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dinet.Module.Challenge.Application.Contract;

namespace Dinet.Module.Challenge.Application.Orders.GetOrder
{
    public class GetOrderQuery: IQuery<OperationResult<GetOrderResult>>
    {
        public GetOrderQuery(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get;  } 
        

    }
}
