using ApplicationCore.Entities.BasketAggregate;
using ApplicationCore.Entities.CatalogAggregate;
using ApplicationCore.Entities.OrderAggregate;
using ApplicationCore.Entities.WishlistAggregate;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Interfaces;

public interface IApplicationDbContext
{
    DbSet<CatalogItem> CatalogItems { get; set; }
    DbSet<CatalogBrand> CatalogBrands { get; set; }
    DbSet<CatalogType> CatalogTypes { get; set; }
    DbSet<CatalogItemReview> Reviews { get; set; }
    DbSet<Cart> Carts { get; set; }
    DbSet<CartItem> CartItems { get; set; }
    DbSet<Wishlist> Wishlists { get; set; }
    DbSet<WishlistItem> WishlistItems { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<OrderItem> OrderItems { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}