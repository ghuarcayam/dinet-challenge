using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinet.Module.Challenge.Domain.Orders.Rules
{
    internal class OrderItMustContainItemsRule : IBusinessRule
    {
        readonly ReadOnlyCollection<OrderItem> _items;
        public OrderItMustContainItemsRule(ReadOnlyCollection<OrderItem> items)
        {
            _items = items;
        }
        public string Message => "The order does not contain items";

        public bool IsBroken()
        {
            return !_items.Any();
        }
    }
}
