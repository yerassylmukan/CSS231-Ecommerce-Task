namespace ApplicationCore.Entities.OrderAggregate;

public class ShippingMethod
{
    public ShippingMethod(string name, decimal cost, TimeSpan deliveryTime)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentException("Name cannot be null or empty.");
        if (cost < 0) throw new ArgumentException("Cost cannot be negative.");

        Name = name;
        Cost = cost;
        DeliveryTime = deliveryTime;
    }

    public string Name { get; private set; }
    public decimal Cost { get; private set; }
    public TimeSpan DeliveryTime { get; private set; }
}