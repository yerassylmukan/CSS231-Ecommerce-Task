using ApplicationCore.Entities.WishlistAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class WishlistConfig : IEntityTypeConfiguration<Wishlist>
{
    public void Configure(EntityTypeBuilder<Wishlist> builder)
    {
        builder.HasKey(w => w.Id);

        builder.Property(w => w.Id).ValueGeneratedOnAdd();

        builder.Property(w => w.UserId)
            .IsRequired();
    }
}