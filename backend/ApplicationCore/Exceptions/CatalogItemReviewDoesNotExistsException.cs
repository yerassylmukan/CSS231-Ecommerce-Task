namespace ApplicationCore.Exceptions;

public class CatalogItemReviewDoesNotExistsException : Exception
{
    public CatalogItemReviewDoesNotExistsException() : base("Review does not exists")
    {
    }
}