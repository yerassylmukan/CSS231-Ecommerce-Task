using ApplicationCore.CustomMappers;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Services;

public class CartService : ICartService
{
    private readonly IApplicationDbContext _context;

    public CartService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CartDTO> GetCartAsync(string userId, CancellationToken cancellationToken)
    {
        var cart = await _context.Carts.Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);

        if (cart == null)
        {
            cart = new Cart
            {
                UserId = userId
            };
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return cart.MapToDTO();
    }

    public async Task<CartDTO> AddItemToCartAsync(string userId, int catalogItemId, CancellationToken cancellationToken,
        int quantity = 1)
    {
        var cart = await _context.Carts.Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);

        if (cart == null)
        {
            cart = new Cart
            {
                UserId = userId
            };
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync(cancellationToken);
        }

        var item = await _context.CatalogItems.Include(ci => ci.Reviews)
            .FirstOrDefaultAsync(ci => ci.Id == catalogItemId, cancellationToken);

        if (item == null) throw new CatalogItemDoesNotExistsException(catalogItemId);

        if (item.StockQuantity < 1)
            throw new OutOfStockException(item.Id);

        var cartItem = cart.Items.FirstOrDefault(ci => ci.CatalogItemId == catalogItemId);

        if (cartItem != null)
        {
            cartItem.Quantity += quantity;
        }
        else
        {
            cartItem = new CartItem
            {
                CatalogItemId = catalogItemId,
                UnitPrice = item.Price,
                Quantity = quantity,
                CartId = cart.Id,
                ProductName = item.Name,
                PictureUrl = item.PictureUrl
            };
            cart.Items.Add(cartItem);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return cart.MapToDTO();
    }

    public async Task<CartDTO> UpdateItemQuantityAsync(string userId, int catalogItemId, int quantity,
        CancellationToken cancellationToken)
    {
        var cart = await _context.Carts.Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);

        if (cart == null)
            throw new CartDoesNotExistsException(catalogItemId);

        var cartItem = cart.Items.FirstOrDefault(ci => ci.CatalogItemId == catalogItemId);

        if (cartItem == null)
            throw new CartItemDoesNotExistsException(catalogItemId);

        if (quantity < 1)
            cart.Items.Remove(cartItem);
        else
            cartItem.Quantity = quantity;

        await _context.SaveChangesAsync(cancellationToken);

        return cart.MapToDTO();
    }

    public async Task RemoveItemFromCartAsync(string userId, int catalogItemId, CancellationToken cancellationToken)
    {
        var cart = await _context.Carts.Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);

        if (cart == null) throw new CartDoesNotExistsException(catalogItemId);

        var cartItem = cart.Items.FirstOrDefault(ci => ci.CatalogItemId == catalogItemId);

        if (cartItem == null) throw new CartItemDoesNotExistsException(catalogItemId);

        cart.Items.Remove(cart.Items.First(ci => ci.CatalogItemId == catalogItemId));
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveCartAsync(int cartId, CancellationToken cancellationToken)
    {
        var cart = await _context.Carts.Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.Id == cartId, cancellationToken);

        if (cart == null) throw new CartDoesNotExistsException(cartId);

        _context.Carts.Remove(cart);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveCartByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        var cart = await _context.Carts.Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);

        if (cart == null) throw new CartDoesNotExistsException();

        _context.Carts.Remove(cart);
        await _context.SaveChangesAsync(cancellationToken);
    }
}