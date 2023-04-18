using Core.Models;
using TrananMVC.ViewModel;
using TrananMVC.Interface;

namespace TrananMVC.Service;

public class ReviewService : IReviewService
{
    private readonly Core.Interface.IService<Review> _coreReviewService;
    private readonly Core.Interface.IService<Reservation> _coreReservationService;
    private readonly Core.Interface.IReservationService _coreReservationService2;

    public ReviewService(
        Core.Interface.IService<Review> coreReviewService,
        Core.Interface.IService<Reservation> coreReservationService,
        Core.Interface.IReservationService coreReservationService2
    )
    {
        _coreReservationService = coreReservationService;
        _coreReviewService = coreReviewService;
        _coreReservationService2 = coreReservationService2;
    }

    public async Task<ReviewViewModel> CreateReview(ReviewViewModel reviewViewModel)
    {
        var review = Mapper.GenerateReview(reviewViewModel);
        var createdReview = await _coreReviewService.Create(review);
        if (createdReview == null)
        {
            return new ReviewViewModel();
        }
        return Mapper.GenerateReviewAsViewModel(createdReview);
    }

    public async Task<List<ReviewViewModel>> GetReviewsByMovieAsync(int movieId)
    {
        try
        {
            var allReviews = await _coreReviewService.Get();
            if (allReviews == null)
            {
                return new List<ReviewViewModel>();
            }
            var reviewsByMovieId = allReviews.Where(r => r.MovieId == movieId).ToList();
            if (reviewsByMovieId == null)
            {
                return new List<ReviewViewModel>();
            }
            return reviewsByMovieId.Select(r => Mapper.GenerateReviewAsViewModel(r)).ToList();
        }
        catch (Exception)
        {
            return new List<ReviewViewModel>();
        }
    }
}
