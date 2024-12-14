using ApplicationCore.CustomMappers;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Services;

public class CatalogItemReviewService : ICatalogItemReviewService
{
    private readonly IApplicationDbContext _context;

    public CatalogItemReviewService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CatalogItemReviewDTO>> GetCatalogItemReviewsAsync(CancellationToken cancellationToken)
    {
        var review = await _context.Reviews.ToListAsync(cancellationToken);

        var result = review.Select(r => r.MapToDTO());

        return result;
    }

    public async Task<CatalogItemReviewDTO> GetCatalogItemReviewByIdAsync(int id, CancellationToken cancellationToken)
    {
        var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

        if (review == null)
            throw new CatalogItemReviewDoesNotExistsException();

        return review.MapToDTO();
    }

    public async Task<CatalogItemReviewDTO> GetCatalogItemReviewByUserIdAsync(string userId,
        CancellationToken cancellationToken)
    {
        var review = await _context.Reviews.FirstOrDefaultAsync(r => r.UserId == userId, cancellationToken);

        if (review == null)
            throw new CatalogItemReviewDoesNotExistsException();

        return review.MapToDTO();
    }

    public async Task<CatalogItemReviewDTO> GetCatalogItemReviewByCatalogItemIdAsync(int catalogItemId,
        CancellationToken cancellationToken)
    {
        var review =
            await _context.Reviews.FirstOrDefaultAsync(r => r.CatalogItemId == catalogItemId, cancellationToken);

        if (review == null)
            throw new CatalogItemReviewDoesNotExistsException();

        return review.MapToDTO();
    }

    public async Task<CatalogItemReviewDTO> CreateCatalogItemReviewAsync(string userId, decimal rating,
        string reviewText, int catalogItemId,
        CancellationToken cancellationToken)
    {
        var reviewExists = _context.Reviews.Any(r => r.UserId == userId && r.CatalogItemId == catalogItemId);

        if (reviewExists) throw new CatalogItemReviewAlreadyExistsException();

        var item = await _context.CatalogItems.Include(ci => ci.Reviews)
            .FirstOrDefaultAsync(c => c.Id == catalogItemId, cancellationToken);

        if (item == null) throw new CatalogItemDoesNotExistsException(catalogItemId);

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

    public async Task UpdateCatalogItemReviewAsync(int id, decimal rating, string reviewText,
        CancellationToken cancellationToken)
    {
        var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

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
            review.Rating = rating;
            isChanged = true;
        }

        if (isChanged) await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteCatalogItemReviewAsync(int id, CancellationToken cancellationToken)
    {
        var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

        if (review == null)
            throw new CatalogItemReviewDoesNotExistsException();

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync(cancellationToken);
    }
}