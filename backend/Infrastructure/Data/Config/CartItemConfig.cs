using ApplicationCore.Entities.BasketAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class CartItemConfig : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable("CartItem");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder
            .HasOne(ci => ci.CatalogItem)
            .WithMany()
            .HasForeignKey(ci => ci.CatalogItemId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}