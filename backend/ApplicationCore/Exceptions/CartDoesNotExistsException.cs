namespace ApplicationCore.Exceptions;

public class CartDoesNotExistsException : Exception
{
    public CartDoesNotExistsException(int id) : base($"Cart with id {id} does not exists")
    {
    }

    public CartDoesNotExistsException() : base("Cart with does not exists")
    {
    }
}