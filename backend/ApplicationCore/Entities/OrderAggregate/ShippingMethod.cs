namespace ApplicationCore.Entities.OrderAggregate;

public class ShippingMethod // ValueObject
{
    public ShippingMethod(string name, decimal cost, TimeSpan deliveryTime)
    {
        Name = name;
        Cost = cost;
        DeliveryTime = deliveryTime;
    }

    public string Name { get; set; }
    public decimal Cost { get; set; }
    public TimeSpan DeliveryTime { get; set; }
}