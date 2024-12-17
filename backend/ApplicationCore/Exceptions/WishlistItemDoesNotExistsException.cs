namespace ApplicationCore.Exceptions;

public class WishlistItemDoesNotExistsException : Exception
{
    public WishlistItemDoesNotExistsException(int id) : base($"Wishlist item with id: {id} does not exists")
    {
    }

    public WishlistItemDoesNotExistsException() : base("Wishlist item does not exists")
    {
    }
}