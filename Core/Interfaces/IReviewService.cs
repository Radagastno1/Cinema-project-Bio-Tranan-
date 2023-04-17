using Core.Models;

namespace Core.Interface;

public interface IReviewService
{
    public Task<Review> GetReviewByReservationCode(int reservationCode);
}
