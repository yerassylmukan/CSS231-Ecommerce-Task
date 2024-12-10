namespace ApplicationCore.Entities.CatalogAggregate;

public class CatalogItem : BaseEntity
{
    public CatalogItem()
    {
    }

    public CatalogItem(int catalogTypeId, int catalogBrandId, string name, string description, decimal price,
        string pictureUrl, int initialStock)
    {
        if (initialStock <= 0) throw new ArgumentException("Initial stock must be greater than zero.");
        CatalogTypeId = catalogTypeId;
        CatalogBrandId = catalogBrandId;
        Name = name;
        Description = description;
        Price = price;
        PictureUrl = pictureUrl;
        StockQuantity = initialStock;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public string PictureUrl { get; private set; }
    public int CatalogTypeId { get; private set; }
    public CatalogType? CatalogType { get; private set; }
    public int CatalogBrandId { get; private set; }
    public CatalogBrand? CatalogBrand { get; private set; }
    public int StockQuantity { get; private set; }
    private readonly List<CatalogItemReview> _reviews = new();
    public IReadOnlyCollection<CatalogItemReview> Reviews => _reviews.AsReadOnly();

    public void UpdateDetails(CatalogItemDetails catalogItemDetails)
    {
        if (!string.IsNullOrWhiteSpace(catalogItemDetails.Name))
            throw new ArgumentException("Name cannot be empty.", nameof(catalogItemDetails.Name));

        if (!string.IsNullOrWhiteSpace(catalogItemDetails.Description))
            throw new ArgumentException("Description cannot be empty.", nameof(catalogItemDetails.Description));

        if (catalogItemDetails.Price <= 0)
            throw new ArgumentException("Price cannot be less than zero.", nameof(catalogItemDetails.Price));

        Name = catalogItemDetails.Name;
        Description = catalogItemDetails.Description;
        Price = catalogItemDetails.Price;
    }

    public void UpdateCatalogBrand(int catalogBrandId)
    {
        CatalogBrandId = catalogBrandId;
    }

    public void UpdateCatalogType(int catalogTypeId)
    {
        CatalogTypeId = catalogTypeId;
    }

    public void UpdatePictureUrl(string pictureUrl)
    {
        if (string.IsNullOrEmpty(pictureUrl))
        {
            PictureUrl = string.Empty;
            return;
        }

        PictureUrl = pictureUrl;
    }

    public void UpdateStock(int quantity)
    {
        if (quantity < 0 && StockQuantity + quantity < 0)
            throw new InvalidOperationException("Not enough stock available.");

        StockQuantity += quantity;
    }

    public void AddReviews(int catalogItemId, string userId, decimal rating, string reviewText)
    {
        var review = new CatalogItemReview(catalogItemId, userId, rating, reviewText);
        _reviews.Add(review);
    }

    public void UpdateReviews(int reviewId, decimal? rating, string reviewText)
    {
        var review = Reviews.FirstOrDefault(r => r.Id == reviewId);

        if (review == null) throw new ArgumentException("Review not found.", nameof(reviewId));

        if (rating.HasValue)
        {
            if (rating < 0 || rating > 5)
                throw new ArgumentException("Rating must be between 0 and 5.", nameof(rating));

            review.UpdateReview(rating, reviewText);
        }

        if (!string.IsNullOrEmpty(reviewText)) review.UpdateReview(rating, reviewText);
    }

    public readonly record struct CatalogItemDetails
    {
        public CatalogItemDetails(string? name, string? description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }

        public string? Name { get; }
        public string? Description { get; }
        public decimal Price { get; }
    }
}