namespace ApplicationCore.Exceptions;

public class CatalogTypeAlreadyExistsException : Exception
{
    public CatalogTypeAlreadyExistsException(string name) : base($"Catalog Type {name} is already exists")
    {
    }
}