namespace ApplicationCore.Entities;

public class ShippingDetails
{
    public ShippingDetails(string addressToShip, string phoneNumber)
    {
        AddressToShip = addressToShip;
        PhoneNumber = phoneNumber;
    }

    public string AddressToShip { get; set; }
    public string PhoneNumber { get; set; }
}