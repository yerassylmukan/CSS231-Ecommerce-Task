namespace ApplicationCore.Exceptions;

public class CatalogItemDoesNotExistsException : Exception
{
    public CatalogItemDoesNotExistsException(int id) : base($"Catalog item with id {id} does not exists")
    {
    }

    public CatalogItemDoesNotExistsException(string catalogBrandOrTypeName) : base(
        $"Catalog item with {catalogBrandOrTypeName} does not exists")
    {
    }
}