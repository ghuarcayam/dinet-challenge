using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinet.Module.Challenge.Application.Orders.GetOrder
{
    public class GetOrderResult
    {
        public Guid OrderId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Cliente { get; set; }
        public decimal Total { get; set; }

        public List<GetOrderResultItem> Items { get; set; }

        public class GetOrderResultItem 
        { 
            public Guid Id { get; set; }
            public string Product { get; set; }
            public int Cantidad { get; set; }
            public decimal PrecioUnitario { get; set; }
            public decimal SubTotal { get; set; }
        }
    }
}
