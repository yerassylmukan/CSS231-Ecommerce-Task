using ApplicationCore.CustomMappers;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Services;

public class WishlistService : IWishlistService
{
    private readonly IApplicationDbContext _context;

    public WishlistService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<WishlistDTO> GetWishlistAsync(string userId, CancellationToken cancellationToken)
    {
        var wishlist = await _context.Wishlists.Include(w => w.Items)
            .FirstOrDefaultAsync(w => w.UserId == userId, cancellationToken);

        if (wishlist == null)
        {
            wishlist = new Wishlist
            {
                UserId = userId
            };
            _context.Wishlists.Add(wishlist);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return wishlist.MapToDTO();
    }

    public async Task<WishlistDTO> AddItemToWishlistAsync(string userId, int catalogItemId,
        CancellationToken cancellationToken)
    {
        var wishlist = await _context.Wishlists.Include(w => w.Items)
            .FirstOrDefaultAsync(w => w.UserId == userId, cancellationToken);

        if (wishlist == null)
        {
            wishlist = new Wishlist
            {
                UserId = userId
            };
            _context.Wishlists.Add(wishlist);
            await _context.SaveChangesAsync(cancellationToken);
        }

        var item = await _context.CatalogItems.Include(ci => ci.Reviews)
            .FirstOrDefaultAsync(ci => ci.Id == catalogItemId, cancellationToken);

        if (item == null) throw new CatalogItemDoesNotExistsException(catalogItemId);

        var alreadyExists = wishlist.Items.Any(ci => ci.Id == catalogItemId);

        if (alreadyExists)
            throw new CatalogItemAlreadyInWishlistException(catalogItemId);

        var wishlistItem = new WishlistItem
        {
            CatalogItemId = catalogItemId,
            WishlistId = wishlist.Id,
            ProductName = item.Name,
            PictureUrl = item.PictureUrl
        };

        wishlist.Items.Add(wishlistItem);
        await _context.SaveChangesAsync(cancellationToken);

        return wishlist.MapToDTO();
    }

    public async Task RemoveItemFromWishlistAsync(string userId, int catalogItemId, CancellationToken cancellationToken)
    {
        var wishlist = await _context.Wishlists.Include(w => w.Items)
            .FirstOrDefaultAsync(w => w.UserId == userId, cancellationToken);

        if (wishlist == null)
            throw new WishlistDoesNotExistsException();

        var wishlistItem = wishlist.Items.FirstOrDefault(i => i.CatalogItemId == catalogItemId);

        if (wishlistItem == null)
            throw new WishlistItemDoesNotExistsException(catalogItemId);

        wishlist.Items.Remove(wishlist.Items.First(i => i.CatalogItemId == catalogItemId));
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveWishlistAsync(int wishlistId, CancellationToken cancellationToken)
    {
        var wishlist = await _context.Wishlists.Include(w => w.Items)
            .FirstOrDefaultAsync(w => w.Id == wishlistId, cancellationToken);

        if (wishlist == null)
            throw new WishlistDoesNotExistsException(wishlistId);

        _context.Wishlists.Remove(wishlist);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveWishlistByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        var wishlist = await _context.Wishlists.Include(w => w.Items)
            .FirstOrDefaultAsync(w => w.UserId == userId, cancellationToken);

        if (wishlist == null)
            throw new WishlistDoesNotExistsException();

        _context.Wishlists.Remove(wishlist);
        await _context.SaveChangesAsync(cancellationToken);
    }
}