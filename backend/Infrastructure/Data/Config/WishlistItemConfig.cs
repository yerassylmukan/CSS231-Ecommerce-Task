using ApplicationCore.Entities.WishlistAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class WishlistItemConfig : IEntityTypeConfiguration<WishlistItem>
{
    public void Configure(EntityTypeBuilder<WishlistItem> builder)
    {
        builder.ToTable("WishlistItem");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder
            .HasOne(wi => wi.CatalogItem)
            .WithMany()
            .HasForeignKey(wi => wi.CatalogItemId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}