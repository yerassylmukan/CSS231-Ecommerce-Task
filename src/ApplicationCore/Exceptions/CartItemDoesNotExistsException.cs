namespace ApplicationCore.Exceptions;

public class CartItemDoesNotExistsException : Exception
{
    public CartItemDoesNotExistsException(int id) : base($"Catalog Item with id: {id} is out of stock.")
    {
    }
}