using Dinet.Module.Challenge.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Dinet.Module.Challenge.Infraestructure.Domain.Orders
{
    public class OrderDetailsEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrdenDetalles");

            builder.HasKey("_id");

            builder.Property<Guid>("_id").HasColumnName("Id").ValueGeneratedNever();
            builder.Property<string>("_producto").HasColumnName("Producto").IsRequired().HasMaxLength(100);
            builder.Property<int>("_cantidad").HasColumnName("Cantidad").IsRequired();
            builder.Property<decimal>("_precioUnitario").HasColumnName("PrecioUnitario").IsRequired();
            builder.Property<decimal>("_subTotal").HasColumnName("SubTotal").IsRequired();
            builder.Property<Guid>("_orderId").HasColumnName("OrderId").IsRequired(); // clave foránea
        }
    }
}
