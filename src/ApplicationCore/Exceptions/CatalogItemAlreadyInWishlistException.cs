namespace ApplicationCore.Exceptions;

public class CatalogItemAlreadyInWishlistException : Exception
{
    public CatalogItemAlreadyInWishlistException(int catalogItemId) : base(
        $"Catalog Item with id {catalogItemId} is already in wishlist")
    {
    }
}