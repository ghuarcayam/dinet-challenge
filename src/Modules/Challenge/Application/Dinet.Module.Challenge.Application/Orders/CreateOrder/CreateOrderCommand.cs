using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dinet.Module.Challenge.Application.Contract;

namespace Dinet.Module.Challenge.Application.Orders.CreateOrder
{
    public class CreateOrderCommand: ICommand<OperationResult<Guid>>
    {
        public CreateOrderCommand(string cliente, DateTime fechaCreacion)
        {
            Cliente = cliente;
            OrdenDetails = new List<OrderItemCommand>();
            FechaCreacion = fechaCreacion;
        }

        public string Cliente { get; }
        public DateTime FechaCreacion { get; }
        public List<OrderItemCommand> OrdenDetails { get; }
        
        public class OrderItemCommand 
        {
            public OrderItemCommand(string producto, int cantidad, decimal precioUnitario) 
            {
                Producto = producto;
                Cantidad = cantidad;
                PrecioUnitario = precioUnitario;

            }
            public string Producto { get; }
            public int Cantidad { get; }
            public decimal PrecioUnitario { get; }
        }
    }
}
