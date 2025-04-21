using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Dinet.Module.Challenge.Domain.Orders;

namespace Dinet.Module.Challenge.Infraestructure
{
    internal class ChallengeContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public ChallengeContext(DbContextOptions options)
    : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}
