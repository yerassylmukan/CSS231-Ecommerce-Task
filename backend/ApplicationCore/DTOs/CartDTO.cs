namespace ApplicationCore.DTOs;

public class CartDTO
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public List<CartItemDTO> Items { get; set; }
    public decimal TotalPrice => Items.Sum(item => item.TotalPrice);
}