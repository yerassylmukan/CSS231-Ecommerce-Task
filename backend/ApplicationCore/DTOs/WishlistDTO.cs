namespace ApplicationCore.DTOs;

public class WishlistDTO
{
    public int Id { get; set; }
    public string UserId { get; set; }

    public List<WishlistItemDTO> Items { get; set; }
}