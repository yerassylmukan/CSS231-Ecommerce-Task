using ApplicationCore.Entities.CatalogAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class CatalogItemReviewConfig : IEntityTypeConfiguration<CatalogItemReview>
{
    public void Configure(EntityTypeBuilder<CatalogItemReview> builder)
    {
        builder.ToTable("Reviews");

        builder.Property(r => r.Id)
            .UseHiLo("review_hilo")
            .IsRequired();

        builder.Property(r => r.UserId)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(r => r.CatalogItemId)
            .IsRequired();

        builder.Property(r => r.Rating)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(r => r.ReviewText)
            .IsRequired()
            .HasMaxLength(256);

        builder.HasOne(r => r.CatalogItem)
            .WithMany(c => c.Reviews)
            .HasForeignKey(r => r.CatalogItemId);
    }
}