using Dinet.Module.Challenge.Domain.Orders.Rules;
using System.Collections.ObjectModel;

namespace Dinet.Module.Challenge.Domain.Orders
{
    public class Order : DomainEntity
    {
        private Guid _id;
        private DateTime _fechaCreacion;
        private string _cliente;
        private decimal _total;
        private List<OrderItem> _orderDetails = new();
        private Order()
        {
            //EF
        }
        public Guid Id { get => _id; }
        public DateTime FechaCreacion { get => _fechaCreacion; }
        public string Cliente { get => _cliente; }
        public decimal Total { get => _total; }
        public ReadOnlyCollection<OrderItem> OrderDetails()  => _orderDetails.AsReadOnly(); 

        public Order(DateTime fechaCreacion, string cliente)
        {
            _id = Guid.NewGuid();
            _fechaCreacion = fechaCreacion;
            _cliente = cliente;

        }

        public OrderItem AddItem(string producto, int cantidad, decimal precioUnitario)
        {
            var item = OrderItem.Create(_id, producto, cantidad, precioUnitario);

            _total += item.SubTotal;

            CheckRule(new OrderItemValuesMustBePositive(item));

            this._orderDetails.Add(item);

            return item;
        }

        public OrderItem ModifyItem(Guid itemId, string producto, int cantidad, decimal precioUnitario) 
        {
            var item = this._orderDetails.FirstOrDefault(x => x.Id == itemId);
            if (item != null)
            {
                _total -= item.SubTotal;
                item.Modify(producto, cantidad, precioUnitario);
                _total += item.SubTotal;
                CheckRule(new OrderItemValuesMustBePositive(item));
            }
            return item;
        }
        public OrderItem RemoveById(Guid itemId)
        {
            var item = this._orderDetails.FirstOrDefault(x => x.Id == itemId);
            if (item != null)
            {
                _total -= item.SubTotal;
                _orderDetails.Remove(item);
            }
            return item;
        }

        public void Modify(DateTime fechaCreacion, string cliente) 
        {
            _fechaCreacion = fechaCreacion;
            _cliente = cliente;
        }

        public void EnsureHasItems() 
        {
            CheckRule(new OrderItMustContainItemsRule(OrderDetails()));
        }

    }
}
