using ApplicationCore.Entities.CatalogAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class CatalogBrandConfig : IEntityTypeConfiguration<CatalogBrand>
{
    public void Configure(EntityTypeBuilder<CatalogBrand> builder)
    {
        builder.HasKey(cb => cb.Id);

        builder.Property(cb => cb.Id).ValueGeneratedOnAdd();

        builder.Property(cb => cb.Brand)
            .HasMaxLength(256)
            .IsRequired();
    }
}