namespace ApplicationCore.Exceptions;

public class CatalogBrandAlreadyExistsException : Exception
{
    public CatalogBrandAlreadyExistsException(string name) : base($"Catalog Brand {name} is already exists")
    {
    }
}