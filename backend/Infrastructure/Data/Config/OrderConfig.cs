using ApplicationCore.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class OrderConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        var navigation = builder.Metadata.FindNavigation(nameof(Order.Items));

        navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Property(o => o.UserId)
            .IsRequired()
            .HasMaxLength(256);

        builder.OwnsOne(o => o.ShippingMethod, a =>
        {
            a.WithOwner();

            a.Property(s => s.Name)
                .HasMaxLength(30)
                .IsRequired();
            
            a.Property(s => s.Cost)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            
            a.Property(s => s.DeliveryTime)
                .IsRequired();
        });

        builder.Navigation(x => x.ShippingMethod).IsRequired();
    }
}