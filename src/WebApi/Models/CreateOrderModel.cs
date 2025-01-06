using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class CreateOrderModel
{
    [Required] public string DeliveryName { get; set; }

    [Required] public decimal DeliveryCost { get; set; }

    [Required] public int DeliveryTime { get; set; }

    [Required] public string AddressToShip { get; set; }

    [Required] public string PhoneNumber { get; set; }
}