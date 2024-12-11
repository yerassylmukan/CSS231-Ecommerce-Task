using ApplicationCore.Entities.CatalogAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class CatalogTypeConfig : IEntityTypeConfiguration<CatalogType>
{
    public void Configure(EntityTypeBuilder<CatalogType> builder)
    {
        builder.HasKey(ct => ct.Id);

        builder.Property(ct => ct.Id).ValueGeneratedOnAdd();

        builder.Property(ct => ct.Type)
            .HasMaxLength(256)
            .IsRequired();
    }
}