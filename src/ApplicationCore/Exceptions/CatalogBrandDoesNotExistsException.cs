namespace ApplicationCore.Exceptions;

public class CatalogBrandDoesNotExistsException : Exception
{
    public CatalogBrandDoesNotExistsException(int id) : base($"Brand {id} does not exist.")
    {
    }

    public CatalogBrandDoesNotExistsException(string name) : base($"Brand {name} does not exist.")
    {
    }
}