namespace ApplicationCore.Exceptions;

public class WishlistDoesNotExistsException : Exception
{
    public WishlistDoesNotExistsException(int id) : base($"Wishlist with id: {id} does not exists")
    {
    }

    public WishlistDoesNotExistsException() : base("Wishlist does not exists")
    {
    }
}