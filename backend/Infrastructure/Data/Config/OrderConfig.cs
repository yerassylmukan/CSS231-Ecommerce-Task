using ApplicationCore.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class OrderConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id).ValueGeneratedOnAdd();

        builder.Property(o => o.UserId).IsRequired();

        builder.OwnsOne(o => o.ShippingMethod, sm =>
        {
            sm.Property(s => s.Name).HasMaxLength(256).IsRequired();
            sm.Property(s => s.Cost).HasColumnType("decimal(18,2)");
            sm.Property(s => s.DeliveryTime);
        });

        builder.Navigation(o => o.ShippingMethod).IsRequired();
    }
}