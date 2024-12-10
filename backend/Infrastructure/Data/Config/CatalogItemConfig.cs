using ApplicationCore.Entities.CatalogAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class CatalogItemConfig : IEntityTypeConfiguration<CatalogItem>
{
    public void Configure(EntityTypeBuilder<CatalogItem> builder)
    {
        builder.ToTable("CatalogItem");
        
        builder.HasKey(ci => ci.Id);
        
        builder.Property(ci => ci.Id).ValueGeneratedOnAdd();
        
        builder
            .HasOne(ci => ci.CatalogType)
            .WithMany()
            .HasForeignKey(ci => ci.CatalogTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasOne(ci => ci.CatalogBrand)
            .WithMany()
            .HasForeignKey(ci => ci.CatalogBrandId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}