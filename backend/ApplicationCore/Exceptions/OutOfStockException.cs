namespace ApplicationCore.Exceptions;

public class OutOfStockException : Exception
{
    public OutOfStockException(int id) : base($"Catalog Item with id: {id} is out of stock.")
    {
    }
}