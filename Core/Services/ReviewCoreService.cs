using Core.Interface;
using Core.Models;

namespace Core.Services;

public class ReviewService : IService<Review>, IReviewService
{
    private IRepository<Review> _reviewRepository;

    public ReviewService(IRepository<Review> reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<IEnumerable<Review>> Get()
    {
        try
        {
            var reviews = await _reviewRepository.GetAsync();
            if (reviews == null)
            {
                return Enumerable.Empty<Review>();
            }
            return reviews;
        }
        catch (Exception)
        {
            throw new Exception("Failed to get reviews.");
        }
    }

    public async Task<Review> GetReviewByReservationCode(int reservationCode)
    {
        try
        {
            var reviews = await _reviewRepository.GetAsync();
            if (reviews == null)
            {
                return null;
            }
            var review = reviews.Where(r => r.ReservationCode == reservationCode).First();
            if (review == null)
            {
                return new Review();
            }
            return review;
        }
        catch (Exception)
        {
            throw new Exception("Failed to get review by reseration code.");
        }
    }

    public async Task<Review> Create(Review review)
    {
        try
        {
            var createdReview = await _reviewRepository.CreateAsync(review);
            return createdReview;
        }
        catch (Exception)
        {
            throw new Exception("Failed to create review.");
        }
    }

    Task<Review> IService<Review>.GetById(int id)
    {
        throw new NotImplementedException();
    }

    Task<Review> IService<Review>.Update(Review obj)
    {
        throw new NotImplementedException();
    }

    Task IService<Review>.DeleteById(int id)
    {
        throw new NotImplementedException();
    }
}
