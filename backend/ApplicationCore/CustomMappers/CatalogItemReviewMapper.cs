using ApplicationCore.DTOs;
using ApplicationCore.Entities.CatalogAggregate;

namespace ApplicationCore.CustomMappers;

public static class CatalogItemReviewMapper
{
    public static CatalogItemReviewDTO MapToDTO(this CatalogItemReview catalogItemReview)
    {
        return new CatalogItemReviewDTO
        {
            Id = catalogItemReview.Id,
            UserId = catalogItemReview.UserId,
            Rating = catalogItemReview.Rating,
            ReviewText = catalogItemReview.ReviewText,
            CreatedTime = catalogItemReview.CreatedTime,
            CatalogItemId = catalogItemReview.CatalogItemId
        };
    }
}