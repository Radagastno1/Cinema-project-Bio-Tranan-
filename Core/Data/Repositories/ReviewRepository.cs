using Microsoft.EntityFrameworkCore;
using Core.Interface;
using Core.Models;

namespace Core.Data.Repository;

public class ReviewRepository : IRepository<Review>
{
    private readonly TrananDbContext _trananDbContext;

    public ReviewRepository(TrananDbContext trananDbContext)
    {
        _trananDbContext = trananDbContext;
    }

    public async Task<Review> CreateAsync(Review review)
    {
        try
        {
            var reservation = await _trananDbContext.Reservations
                .Where(r => r.ReservationCode == review.ReservationCode)
                .FirstAsync();

            if (reservation == null)
            {
                return null;
            }

            var existingReviewOnReservationCode = await _trananDbContext.Reviews
                .Where(r => r.ReservationCode == review.ReservationCode)
                .FirstOrDefaultAsync();

            if (existingReviewOnReservationCode == null)
            {
                var movie = await _trananDbContext.Movies.FindAsync(review.MovieId);

                review.Movie = movie;

                await _trananDbContext.Reviews.AddAsync(review);
                await _trananDbContext.SaveChangesAsync();

                var recentlyAddedReview = await _trananDbContext.Reviews
                    .OrderByDescending(r => r.ReviewId)
                    .FirstOrDefaultAsync();
                return recentlyAddedReview;
            }
            else
            {
                throw new InvalidOperationException(
                    "Review with this reservationcode has allready made a review ."
                );
            }
        }
        catch (InvalidOperationException e)
        {
            if(e.Message == "Review with this reservationcode has allready made a review .")
            {
                throw new InvalidOperationException(e.Message);
            }
            return null;
        }
    }

    public async Task DeleteAsync()
    {
        var reviews = await _trananDbContext.Reviews.ToListAsync();
        _trananDbContext.RemoveRange(reviews);
    }

    public async Task DeleteByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Review>> GetAsync()
    {
        try
        {
            var reviews = await _trananDbContext.Reviews.ToListAsync();
            if (reviews == null)
            {
                return Enumerable.Empty<Review>();
            }
            return reviews;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<Review> GetByIdAsync(int id)
    {
        try
        {
            var review = await _trananDbContext.Reviews.Where(r => r.ReviewId == id).FirstAsync();
            if(review == null)
            {
                return null;
            }
            return review;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<Review> UpdateAsync(Review obj)
    {
        throw new NotImplementedException();
    }
}
