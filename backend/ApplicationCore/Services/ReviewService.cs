using ApplicationCore.CustomMappers;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Services;

public class ReviewService : IReviewService
{
    private readonly IApplicationDbContext _context;

    public ReviewService(IApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<CatalogItemReviewDTO>> GetReviewsAsync(int catalogItemId,
        CancellationToken cancellationToken)
    {
        var review = await _context.Reviews.Where(r => r.CatalogItemId == catalogItemId).ToListAsync(cancellationToken);

        var result = review.Select(r => r.MapToDTO());

        return result;
    }

    public async Task<IEnumerable<CatalogItemReviewDTO>> GetReviewsByUserIdAsync(string userId,
        CancellationToken cancellationToken)
    {
        var review = await _context.Reviews.Where(r => r.UserId == userId).ToListAsync(cancellationToken);

        var result = review.Select(r => r.MapToDTO());

        return result;
    }

    public async Task<CatalogItemReviewDTO> GetReviewByIdAsync(int id, CancellationToken cancellationToken)
    {
        var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

        if (review == null)
            throw new CatalogItemReviewDoesNotExistsException();

        return review.MapToDTO();
    }

    public async Task<CatalogItemReviewDTO> CreateReviewAsync(string userId, decimal rating, string reviewText,
        int catalogItemId,
        CancellationToken cancellationToken)
    {
        var reviewExists = _context.Reviews.Any(r => r.UserId == userId && r.CatalogItemId == catalogItemId);

        if (reviewExists) throw new CatalogItemReviewAlreadyExistsException();

        var item = await _context.CatalogItems.Include(ci => ci.Reviews)
            .FirstOrDefaultAsync(c => c.Id == catalogItemId, cancellationToken);

        if (item == null) throw new CatalogItemDoesNotExistsException(catalogItemId);

        if (rating < 1 || rating > 5)
            throw new ArgumentException("Rating must be between 1 and 5");

        if (string.IsNullOrEmpty(reviewText))
            throw new ArgumentException(nameof(reviewText));

        var review = new CatalogItemReview
        {
            UserId = userId,
            Rating = rating,
            ReviewText = reviewText,
            CatalogItemId = catalogItemId
        };

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync(cancellationToken);

        return review.MapToDTO();
    }

    public async Task UpdateReviewAsync(int id, string userId, decimal rating, string reviewText,
        CancellationToken cancellationToken)
    {
        var review =
            await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId, cancellationToken);

        if (review == null)
            throw new CatalogItemReviewDoesNotExistsException();

        var isChanged = false;

        if (!string.IsNullOrEmpty(review.ReviewText) && review.ReviewText != reviewText)
        {
            review.ReviewText = reviewText;
            isChanged = true;
        }

        if (review.Rating != rating)
        {
            if (rating < 1 || rating > 5)
                throw new ArgumentException("Rating must be between 1 and 5");

            review.Rating = rating;
            isChanged = true;
        }

        if (isChanged) await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteReviewAsync(int id, string userId, CancellationToken cancellationToken)
    {
        var review =
            await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId, cancellationToken);

        if (review == null)
            throw new CatalogItemReviewDoesNotExistsException();

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync(cancellationToken);
    }
}