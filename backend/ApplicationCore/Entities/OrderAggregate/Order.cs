namespace ApplicationCore.Entities.OrderAggregate;

public class Order : BaseEntity
{
    public Order(string userId, int shippingMethodId, ShippingMethod shippingMethod, List<OrderItem> orderItems)
    {
        if (string.IsNullOrEmpty(userId)) throw new ArgumentException("User ID cannot be null or empty.");
        if (shippingMethod == null) throw new ArgumentNullException(nameof(shippingMethod));
        if (orderItems == null || !orderItems.Any()) throw new ArgumentException("Order must have at least one item.");

        UserId = userId;
        ShippingMethodId = shippingMethodId;
        ShippingMethod = shippingMethod;
        OrderItems = orderItems;
        OrderDate = DateTimeOffset.Now;
    }

    public string UserId { get; private set; }
    public DateTimeOffset OrderDate { get; private set; }
    public List<OrderItem> OrderItems { get; }

    public int ShippingMethodId { get; private set; }
    public ShippingMethod ShippingMethod { get; private set; }

    public decimal Subtotal => OrderItems.Sum(oi => oi.TotalPrice);
    public decimal Total => Subtotal + (ShippingMethod?.Cost ?? 0);

    public void UpdateShippingMethod(int shippingMethodId, ShippingMethod shippingMethod)
    {
        if (shippingMethod == null) throw new ArgumentNullException(nameof(shippingMethod));
        ShippingMethodId = shippingMethodId;
        ShippingMethod = shippingMethod;
    }
}