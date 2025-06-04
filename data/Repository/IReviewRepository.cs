using ArtMarketplace.Data;
using ArtMarketplace.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace data.Repository;
public interface IReviewRepository
{
    Task UpdateReview(Guid reviewId, string Review, float Rating);
    Task<Guid> AddReview(Guid productId, string Review, float Rating);
    Task SetReviewResponse(Guid reviewId, string response);
    Task<Review> GetReview(Guid reviewId);
}
public class ReviewRepository(ArtMarketplaceDbContext dbContext) : IReviewRepository
{
    public async Task UpdateReview(Guid reviewId, string Comment, float Rating)
    {
        var review = await dbContext.Reviews.FirstOrDefaultAsync(review => review.Id == reviewId)
            ?? throw new KeyNotFoundException($"review with id {reviewId} not found");
        review.Comment = Comment;
        review.Rating = Rating;
        await dbContext.SaveChangesAsync();
    }
    public async Task<Guid> AddReview(Guid productId, string Comment, float Rating)
    {
        var review = new Review();
        review.Comment = Comment;
        review.Rating = Rating;
        review.ProductId = productId;
        var product = await dbContext.Products.FirstAsync(product => product.Id == productId);
        Guid idReview = (await dbContext.Reviews.AddAsync(review)).Entity.Id;
        product.ReviewId = idReview;
        await dbContext.SaveChangesAsync();
        return idReview;
    }
    public async Task SetReviewResponse(Guid reviewId, string response)
    {
        var review = await dbContext.Reviews.SingleAsync(review => review.Id == reviewId);
        review.Response = response;
        await dbContext.SaveChangesAsync();
    }

    public async Task<Review> GetReview(Guid reviewId)
    {
        return await dbContext.Reviews.SingleAsync(review => review.Id == reviewId);
    }
}
