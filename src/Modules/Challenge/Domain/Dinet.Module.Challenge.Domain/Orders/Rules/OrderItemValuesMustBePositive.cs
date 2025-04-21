using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinet.Module.Challenge.Domain.Orders.Rules
{
    internal class OrderItemValuesMustBePositive : IBusinessRule
    {
        private readonly OrderItem _orderItem;
        internal OrderItemValuesMustBePositive(OrderItem orderItem) 
        {
            _orderItem = orderItem;
        }
        public string Message => "The order item contain values negatives";

        public bool IsBroken()
        {
            return _orderItem.Cantidad<0 ||
                    _orderItem.PrecioUnitario <0;
        }
    }
}
