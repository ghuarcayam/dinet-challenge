using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinet.Module.Challenge.Domain.Orders
{
    public interface IOrderRepository
    {
        Task<Order> GetAsync(Guid OrderId, CancellationToken cancellationToken = default);

        Task<(IEnumerable<Order>, int)> GetAsync(int startAt,
            int size,
            string? cliente,
            DateTime? from,
            DateTime? to,
            string? fieldOrder,
            bool orderDesc,
            CancellationToken cancellationToken = default);

        Task<bool> DetectDuplicates(Guid? ordenId, string cliente, DateTime fechaCreacion, CancellationToken cancellationToken = default);

        Task AddAsync(Order Order, CancellationToken cancellationToken = default);

        Task UpdateAsync(Order Order, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid orderId, CancellationToken cancellationToken = default);
    }
}
