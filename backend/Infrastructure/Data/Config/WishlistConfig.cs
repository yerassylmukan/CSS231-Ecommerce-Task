using ApplicationCore.Entities.WishlistAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class WishlistConfig : IEntityTypeConfiguration<Wishlist>
{
    public void Configure(EntityTypeBuilder<Wishlist> builder)
    {
        builder.ToTable("Wishlists");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder
            .HasMany(w => w.Items)
            .WithOne(wi => wi.Wishlist)
            .HasForeignKey(wi => wi.WishlistId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}