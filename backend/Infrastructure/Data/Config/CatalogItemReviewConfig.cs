using ApplicationCore.Entities.CatalogAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class CatalogItemReviewConfig : IEntityTypeConfiguration<CatalogItemReview>
{
    public void Configure(EntityTypeBuilder<CatalogItemReview> builder)
    {
        builder.ToTable("Reviews");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id).ValueGeneratedOnAdd();

        builder.Property(r => r.UserId)
            .IsRequired();

        builder.Property(r => r.Rating)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(r => r.ReviewText)
            .HasMaxLength(256)
            .IsRequired();

        builder.HasOne(r => r.CatalogItem)
            .WithMany(ci => ci.Reviews)
            .HasForeignKey(r => r.CatalogItemId)
            .IsRequired(false);
    }
}