using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Dinet.Module.Challenge.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Dinet.Module.Challenge.Infraestructure.Domain.Orders
{
    internal class OrderRepository : IOrderRepository
    {
        private readonly ChallengeContext context;
        public OrderRepository(ChallengeContext context) 
        {
            this.context = context;
        }
        public async Task AddAsync(Order Order, CancellationToken cancellationToken = default)
        {
            await context.Orders.AddAsync(Order, cancellationToken);
        }

        public async Task DeleteAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            var o = await GetAsync(orderId, cancellationToken);
            context.Remove(o);
        }

        public async Task<Order> GetAsync(Guid OrderId, CancellationToken cancellationToken = default)
        {
            return await this.context.Orders.FindAsync(new object[] { OrderId }, cancellationToken);
        }

        public async Task<bool> DetectDuplicates(Guid? ordenId, string cliente, DateTime fechaCreacion, CancellationToken cancellationToken = default) 
        {
            var query = context.Orders.AsQueryable();
            query = query.Where(o => o.Cliente.Equals(cliente, StringComparison.CurrentCultureIgnoreCase) && o.FechaCreacion == fechaCreacion);
            if (ordenId.HasValue) query = query.Where(o => o.Id != ordenId.Value);
            var r = await query.AnyAsync(cancellationToken); ;
            return r;

        }
        public async Task<(IEnumerable<Order>, int)> GetAsync(int startAt, 
            int size, 
            string? cliente, 
            DateTime? from, 
            DateTime? to, 
            string? fieldOrder, 
            bool orderDesc, 
            CancellationToken cancellationToken = default)
        {

            var query = context.Orders.AsQueryable();

            // Filtro por cliente
            if (!string.IsNullOrWhiteSpace(cliente))
            {
                query = query.Where(o => o.Cliente.Contains(cliente, StringComparison.CurrentCultureIgnoreCase) );
            }

            // Filtro por rango de fechas
            if (from.HasValue)
            {
                query = query.Where(o => o.FechaCreacion >= from.Value);
            }

            if (to.HasValue)
            {
                query = query.Where(o => o.FechaCreacion <= to.Value);
            }

            // Total antes del paginado
            int total = await query.CountAsync();

            // Ordenamiento dinámico
            if (!string.IsNullOrWhiteSpace(fieldOrder))
            {
                query = fieldOrder switch
                {
                    "FechaCreacion" => orderDesc ? query.OrderByDescending(o => o.FechaCreacion) : query.OrderBy(o => o.FechaCreacion),
                    "Total" => orderDesc ? query.OrderByDescending(o => o.Total) : query.OrderBy(o => o.Total),
                    "Cliente" => orderDesc ? query.OrderByDescending(o => o.Cliente) : query.OrderBy(o => o.Cliente),
                    _ => query.OrderByDescending(o => o.FechaCreacion)
                };
            }
            else
            {
                query = query.OrderByDescending(o => o.FechaCreacion); // por defecto
            }

            // Paginado
            var items = await query
                .Skip(startAt)
                .Take(size)
                .ToListAsync();

            return (items, total);
        }

        public async Task UpdateAsync(Order Order, CancellationToken cancellationToken = default)
        {
             context.Orders.Update(Order);
        }
    }
}
