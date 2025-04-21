using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dinet.Module.Challenge.Domain.Orders;

namespace Dinet.Module.Challenge.Infraestructure.Domain.Orders
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Ordenes");

            builder.HasKey((o)=>o.Id);

            builder.Property(o => o.Id).HasField("_id").UsePropertyAccessMode(PropertyAccessMode.Field).HasColumnName("Id");
            builder.Property(o => o.FechaCreacion).HasField("_fechaCreacion").UsePropertyAccessMode(PropertyAccessMode.Field).HasColumnName("FechaCreacion");
            builder.Property(o => o.Cliente).HasField("_cliente").UsePropertyAccessMode(PropertyAccessMode.Field).HasColumnName("Cliente");
            builder.Property(o => o.Total).HasField("_total").UsePropertyAccessMode(PropertyAccessMode.Field).HasColumnName("Total");
            
            builder.HasMany<OrderItem>("_orderDetails")
              .WithOne()
              .HasForeignKey("_orderId")
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
