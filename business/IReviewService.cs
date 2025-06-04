using ArtMarketplace.Domain.Models;
using data.Repository;
using domain.DTOs.Review;
using System.ComponentModel.DataAnnotations;

namespace business;
public interface IReviewService
{
    Task UpdateReview(ReviewUpdateDto review);
    Task<Guid> CreateReview(ReviewCreateDto review);
    Task RespondToReview(ReviewResponseDto reviewResponseDto);
    Task<Review> GetReview(Guid idReview);

}
public class ReviewService(IReviewRepository reviewRepository) : IReviewService
{
    public async Task<Review> GetReview(Guid idReview)
    {
        return await reviewRepository.GetReview(idReview);
    }

    public async Task RespondToReview(ReviewResponseDto reviewResponseDto)
    {
        await reviewRepository.SetReviewResponse(reviewResponseDto.Id, reviewResponseDto.ResponseComment);
    }

    public async Task UpdateReview(ReviewUpdateDto review)
    {
        if (review.Rating > 5)
        {
            throw new ValidationException($"{review.Rating} is not a acceptable value for a rating");
        }
        await reviewRepository.UpdateReview(review.Id, review.Comment, review.Rating);
    }
    public async Task<Guid> CreateReview(ReviewCreateDto review)
    {
        if (review.Rating > 5)
        {
            throw new ValidationException($"{review.Rating} is not a acceptable value for a rating");
        }
        return await reviewRepository.AddReview(review.ProductId, review.Comment, review.Rating);
    }
}
