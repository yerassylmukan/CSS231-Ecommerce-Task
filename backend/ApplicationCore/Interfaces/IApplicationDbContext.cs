using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

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

    DatabaseFacade Database { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}