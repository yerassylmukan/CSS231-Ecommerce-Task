using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class ShippingMethodModel
{
    [Required] public string deliveryName { get; set; }

    [Required] public decimal deliveryCost { get; set; }

    [Required] public int deliveryTime { get; set; }
}