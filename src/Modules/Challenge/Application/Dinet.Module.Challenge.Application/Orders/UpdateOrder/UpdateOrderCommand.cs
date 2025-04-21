using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dinet.Module.Challenge.Application.Contract;

namespace Dinet.Module.Challenge.Application.Orders.UpdateOrder
{
    public class UpdateOrderCommand: ICommand<OperationResult>
    {
        public UpdateOrderCommand(Guid id, string cliente, DateTime fechaCreacion)
        {
            Cliente = cliente;
            Id = id;
            OrdenDetails = new List<UpdateOrderItemCommand>();
            FechaCreacion = fechaCreacion;
        }

        public Guid Id { get; }
        public string Cliente { get; }
        public DateTime FechaCreacion { get; }
        public List<UpdateOrderItemCommand> OrdenDetails { get; }

        public class UpdateOrderItemCommand
        {
            public UpdateOrderItemCommand(Guid? id, string producto, int cantidad, decimal precioUnitario)
            {
                Id = id;
                Producto = producto;
                Cantidad = cantidad;
                PrecioUnitario = precioUnitario;

            }
            public Guid? Id { get; }
            public string Producto { get; }
            public int Cantidad { get; }
            public decimal PrecioUnitario { get; }
        }
    }
}
