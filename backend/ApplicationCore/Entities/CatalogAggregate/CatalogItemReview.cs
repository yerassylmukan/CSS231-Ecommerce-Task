namespace ApplicationCore.Entities.CatalogAggregate;

public class CatalogItemReview : BaseEntity
{
    public CatalogItemReview()
    {
    }

    public CatalogItemReview(int catalogItemId, string userId, decimal rating, string reviewText)
    {
        if (rating < 0 || rating > 5) throw new ArgumentException("Rating must be between 0 and 5.", nameof(rating));
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("Reviewer name cannot be empty.", nameof(userId));

        CatalogItemId = catalogItemId;
        UserId = userId;
        Rating = rating;
        ReviewText = reviewText;
    }

    public int CatalogItemId { get; private set; }
    public CatalogItem? CatalogItem { get; private set; }
    public string UserId { get; private set; }
    public decimal Rating { get; private set; }
    public string ReviewText { get; private set; }
    public DateTimeOffset CreatedTime { get; private set; } = DateTimeOffset.Now;

    public void UpdateReview(decimal? rating, string reviewText)
    {
        if (rating.HasValue)
        {
            if (rating < 0 || rating > 5)
                throw new ArgumentException("Rating must be between 0 and 5.");
            Rating = rating.Value;
        }

        if (!string.IsNullOrEmpty(reviewText))
            ReviewText = reviewText;
    }
}