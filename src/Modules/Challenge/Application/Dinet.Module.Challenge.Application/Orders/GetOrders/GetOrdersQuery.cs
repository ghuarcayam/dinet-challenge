using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dinet.Module.Challenge.Application.Contract;

namespace Dinet.Module.Challenge.Application.Orders.GetOrders
{
    public class GetOrdersQuery: IQuery<OperationResult<GetOrdersResult>>
    {
        public GetOrdersQuery(int startAt, int size, string? cliente, DateTime? from, DateTime? to, string? fieldOrder, bool orderDesc) 
        {
            StartAt  = startAt;
            Size = size;
            Cliente = cliente;
            From = from;
            To = to;
            FieldOrder = fieldOrder;
            OrderDesc = orderDesc;
        }

        public int StartAt { get; }
        public int Size { get; }
        public string? Cliente { get; }
        public DateTime? From { get; }
        public DateTime? To { get; }
        public string?   FieldOrder { get; }
        public bool OrderDesc { get; }

    }
}
