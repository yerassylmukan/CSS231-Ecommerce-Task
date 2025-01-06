using ApplicationCore.DTOs;

namespace ApplicationCore.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<OrderDTO>> GetOrdersAsync(CancellationToken cancellationToken);
    Task<OrderDTO> GetOrderByIdAsync(int orderId, CancellationToken cancellationToken);
    Task<OrderDTO> GetOrderByUserIdAsync(string userId, CancellationToken cancellationToken);

    Task<OrderDTO> CreateOrderAsync(string userId, string deliveryName, decimal deliveryCost, int deliveryTime,
        string addressToShip, string phoneNumber,
        CancellationToken cancellationToken);

    Task ConfirmOrderAsync(int orderId, CancellationToken cancellationToken);
}