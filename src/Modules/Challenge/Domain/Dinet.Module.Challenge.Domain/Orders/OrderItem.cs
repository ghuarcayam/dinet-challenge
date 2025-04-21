using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinet.Module.Challenge.Domain.Orders
{
    public class OrderItem
    {
        private Guid _id;
        private Guid _orderId;
        private string _producto;
        private int _cantidad;
        private decimal _precioUnitario;
        private decimal _subTotal;
        private OrderItem() { }

        public Guid Id { get => _id; }
        public Guid OrderId { get => _orderId;  }
        public string Producto { get => _producto;  }
        public int Cantidad { get => _cantidad;  }
        public decimal PrecioUnitario { get => _precioUnitario;  }
        public decimal SubTotal { get => _subTotal; }

        internal void Modify(string Ordero, int cantidad, decimal precioUnitario)
        {
            _producto = Ordero;
            _cantidad = cantidad;
            _precioUnitario = precioUnitario;
            _subTotal = precioUnitario * cantidad;
        }

    

        internal static OrderItem  Create(Guid orderId, string producto, int cantidad, decimal precioUnitario)
        {
            var noi = new OrderItem();
            noi._id = Guid.NewGuid();
            noi._orderId = orderId;
            noi._producto = producto;
            noi._cantidad = cantidad;
            noi._precioUnitario = precioUnitario;
            noi._subTotal = precioUnitario * cantidad;
            return noi;
        }
    }
}
