namespace ApplicationCore.DTOs;

public class CartItemDTO
{
    public int Id { get; set; }
    public int CatalogItemId { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public string ProductName { get; set; }
    public string PictureUrl { get; set; }
    public int CartId { get; set; }
    public decimal TotalPrice => Quantity * UnitPrice;
}