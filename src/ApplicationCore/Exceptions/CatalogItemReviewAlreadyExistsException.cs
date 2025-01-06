namespace ApplicationCore.Exceptions;

public class CatalogItemReviewAlreadyExistsException : Exception
{
    public CatalogItemReviewAlreadyExistsException() : base("Catalog Item Review already exists")
    {
    }
}