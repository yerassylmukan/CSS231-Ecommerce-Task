using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class CatalogItemConfig : IEntityTypeConfiguration<CatalogItem>
{
    public void Configure(EntityTypeBuilder<CatalogItem> builder)
    {
        builder.ToTable("CatalogItems");

        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.Id).ValueGeneratedOnAdd();

        builder.Property(ci => ci.Name)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(ci => ci.Description)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(ci => ci.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(ci => ci.PictureUrl)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(ci => ci.StockQuantity)
            .IsRequired();

        builder.HasOne(ci => ci.CatalogBrand)
            .WithMany(cb => cb.CatalogItems)
            .HasForeignKey(ci => ci.CatalogBrandId)
            .IsRequired();

        builder.HasOne(ci => ci.CatalogType)
            .WithMany(ct => ct.CatalogItems)
            .HasForeignKey(ci => ci.CatalogTypeId)
            .IsRequired();

        builder.HasMany(ci => ci.Reviews)
            .WithOne(r => r.CatalogItem)
            .HasForeignKey(r => r.CatalogItemId)
            .IsRequired();
    }
}