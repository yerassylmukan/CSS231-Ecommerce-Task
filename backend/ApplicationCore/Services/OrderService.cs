using ApplicationCore.CustomMappers;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Services;

public class OrderService : IOrderService
{
    private readonly ICartService _cartService;
    private readonly ICatalogItemService _catalogItemService;
    private readonly IApplicationDbContext _context;
    private readonly IEmailSender _emailSender;

    public OrderService(IApplicationDbContext context, ICartService cartService, ICatalogItemService catalogItemService,
        IEmailSender emailSender)
    {
        _context = context;
        _cartService = cartService;
        _catalogItemService = catalogItemService;
        _emailSender = emailSender;
    }

    public async Task<IEnumerable<OrderDTO>> GetOrdersAsync(CancellationToken cancellationToken)
    {
        var orders = await _context.Orders.Include(o => o.Items).ToListAsync(cancellationToken);

        var result = orders.Select(o => o.MapToDTO());

        return result;
    }

    public async Task<OrderDTO> GetOrderByIdAsync(int orderId, CancellationToken cancellationToken)
    {
        var order = await _context.Orders.Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);

        if (order == null) throw new OrderDoesNotExistsException(orderId);

        return order.MapToDTO();
    }

    public async Task<OrderDTO> GetOrderByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        var order = await _context.Orders.Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.UserId == userId, cancellationToken);

        if (order == null) throw new OrderDoesNotExistsException();

        return order.MapToDTO();
    }

    public async Task<OrderDTO> CreateOrderAsync(string userId, string deliveryName, decimal deliveryCost,
        int deliveryTime, CancellationToken cancellationToken)
    {
        var cart = await _context.Carts.Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);

        if (cart == null)
            throw new CartDoesNotExistsException();

        var items = cart.Items;

        var shippingMethod = new ShippingMethod(deliveryName, deliveryCost, TimeSpan.FromMinutes(deliveryTime));

        var order = new Order
        {
            UserId = userId,
            ShippingMethod = shippingMethod
        };

        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            foreach (var item in items)
            {
                var catalogItem =
                    await _context.CatalogItems.Include(ci => ci.Reviews)
                        .FirstOrDefaultAsync(ci => ci.Id == item.CatalogItemId, cancellationToken);

                if (catalogItem == null)
                    throw new CatalogItemDoesNotExistsException(item.CatalogItemId);

                if (catalogItem.StockQuantity < item.Quantity)
                {
                    await _emailSender.EmailSendAsync("admin@gmail.com", $"Running out of product - {catalogItem.Name}",
                        $"I would like to inform you that the product with ID {catalogItem.Id} is running out.",
                        cancellationToken);

                    throw new OutOfStockException(item.Id);
                }
                
                catalogItem.StockQuantity -= item.Quantity;

                if (catalogItem.StockQuantity <= 0)
                {
                    await _emailSender.EmailSendAsync("admin@gmail.com", $"Running out of product - {catalogItem.Name}",
                        $"I would like to inform you that the product with ID {catalogItem.Id} is running out.",
                        cancellationToken);
                    
                    catalogItem.StockQuantity = 0;
                }

                var orderItem = new OrderItem
                {
                    CatalogItemId = item.CatalogItemId,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    ProductName = item.ProductName,
                    OrderId = order.Id
                };
                
                _context.CatalogItems.Update(catalogItem);
                _context.OrderItems.Add(orderItem);
                order.Items.Add(orderItem);
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return order.MapToDTO();
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task ConfirmOrderAsync(int orderId, CancellationToken cancellationToken)
    {
        var order = await _context.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);
        
        if (order == null)
        {
            throw new OrderDoesNotExistsException(orderId);
        }

        var customerId = order.UserId;
        
        await _emailSender.EmailSendByUserIdAsync(customerId, $"Order Confirmation - Order #{order.Id}", $"Order ID: {order.Id}, Order Date: {order.OrderDate}, Delivery Time: {order.ShippingMethod.DeliveryTime}", cancellationToken);
    }
}