namespace ApplicationCore.Exceptions;

public class CatalogTypeDoesNotExistsException : Exception
{
    public CatalogTypeDoesNotExistsException(int id) : base($"Type {id} does not exist.")
    {
    }

    public CatalogTypeDoesNotExistsException(string name) : base($"Type {name} does not exist.")
    {
    }
}