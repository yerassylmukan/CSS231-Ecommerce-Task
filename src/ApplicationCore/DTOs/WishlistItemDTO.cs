namespace ApplicationCore.DTOs;

public class WishlistItemDTO
{
    public int Id { get; set; }
    public int CatalogItemId { get; set; }
    public string ProductName { get; set; }
    public string PictureUrl { get; set; }

    public int WishlistId { get; set; }
}