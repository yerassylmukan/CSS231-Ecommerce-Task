using ApplicationCore.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class OrderConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Order");

        builder.HasKey(o => o.Id);
        
        builder.Property(o => o.Id).ValueGeneratedOnAdd();
        
        builder
            .HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasOne(o => o.ShippingMethod)
            .WithMany()
            .HasForeignKey(o => o.ShippingMethodId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}