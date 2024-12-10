namespace ApplicationCore.Entities.OrderAggregate;

public class Order : BaseEntity
{
    private readonly List<OrderItem> _items = new();

    public Order()
    {
    }

    public Order(string userId, ShippingMethod shippingMethod, List<OrderItem> items)
    {
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentException("User ID cannot be null or empty.", nameof(userId));

        UserId = userId;
        ShippingMethod = shippingMethod;
        _items = items;
    }

    public string UserId { get; private set; }
    public DateTimeOffset OrderDate { get; private set; } = DateTimeOffset.Now;
    public ShippingMethod ShippingMethod { get; private set; }
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public decimal Total()
    {
        var total = 0m;
        foreach (var item in _items) total += item.UnitPrice * item.Units;
        return total;
    }
}