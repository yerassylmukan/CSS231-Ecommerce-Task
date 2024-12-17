namespace ApplicationCore.Exceptions;

public class OrderDoesNotExistsException : Exception
{
    public OrderDoesNotExistsException(int id) : base($"Order with id: {id} does not exists")
    {
    }

    public OrderDoesNotExistsException() : base("Order does not exists")
    {
    }
}