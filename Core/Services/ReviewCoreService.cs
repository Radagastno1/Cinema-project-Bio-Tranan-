using Core.Interface;
using Core.Models;

namespace Core.Services;

public class ReviewCoreService
{
    private IRepository<Review> _reviewRepository;

    public ReviewCoreService(
        IRepository<Review> reviewRepository
    )
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<IEnumerable<Review>> GetAllReviews()
    {
        try
        {
            return await _reviewRepository.GetAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Review> CreateReview(Review review)
    {
        try
        {
            if (review == null)
            {
                throw new InvalidOperationException("Something went wrong with saving the review.");
            }
            else
            {
                var createdReview = await _reviewRepository.CreateAsync(review);
                return createdReview;
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}
