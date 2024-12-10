using ApplicationCore.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItem");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder
            .HasOne(oi => oi.CatalogItem)
            .WithMany()
            .HasForeignKey(oi => oi.CatalogItemId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}