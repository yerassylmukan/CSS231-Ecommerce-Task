using ApplicationCore.Entities.BasketAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class CartConfig : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable("Cart");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder
            .HasMany(c => c.Items)
            .WithOne(ci => ci.Cart)
            .HasForeignKey(ci => ci.CartId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}