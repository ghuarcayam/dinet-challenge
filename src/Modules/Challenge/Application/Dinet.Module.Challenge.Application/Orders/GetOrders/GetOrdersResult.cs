using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinet.Module.Challenge.Application.Orders.GetOrders
{
    public class GetOrdersResult
    {
        public int TotalRows { get; set; }

        public IEnumerable<PaginationOrder> Orders { get; set; }

        public class PaginationOrder 
        {
            public PaginationOrder() 
            {
               
            }
            public Guid Id { get; set; }
            public DateTime FechaCreacion { get; set; }
            public string Cliente { get; set; }
            public decimal Total { get; set; }

            public IEnumerable<PaginationOrderItem> Items { get; set; }
        }

        public class PaginationOrderItem
        {
            public Guid Id { get; set; }
            public string Producto { get; set; }
            public int Cantidad { get; set; }
            public decimal PrecioUnitario { get; set; }
            public decimal SubTotal { get; set; }
        }
    }
}
