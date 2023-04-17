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
            return await _reviewRepository.GetAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Review> GetReviewByReservationCode(int reservationCode)
    {
        try
        {
            var reviews = await _reviewRepository.GetAsync();
            var review = reviews.Where(r => r.ReservationCode == reservationCode).First();
            return review;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Review> Create(Review review)
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
