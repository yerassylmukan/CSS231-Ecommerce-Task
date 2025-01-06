namespace ApplicationCore.Entities;

public class ShippingMethod // value object
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