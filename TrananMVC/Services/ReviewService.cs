using Core.Interface;
using Core.Models;
using TrananMVC.ViewModel;
using TrananMVC.Interface;

namespace TrananMVC.Service;

public class ReviewService 
{
    private readonly IService<Review> _coreReviewService;
    private readonly IService<Reservation> _coreReservationService;
    private readonly IReservationService _coreReservationService2;

    public ReviewService(IService<Review> coreReviewService, IService<Reservation> coreReservationService, IReservationService coreReservationService2)
    {
        _coreReservationService = coreReservationService;
        _coreReviewService = coreReviewService;
        _coreReservationService2 = coreReservationService2;
    }

    public async Task<ReviewViewModel> CreateReview(ReviewViewModel reviewViewModel)
    {
        // var reservation = await _coreReservationService2.GetReservationByReservationCode(reviewViewModel.ReservationCode);
        var review = Mapper.GenerateReview(reviewViewModel);
        var createdReview = await _coreReviewService.Create(review);
        return Mapper.GenerateReviewAsViewModel(createdReview);
    }
    public async Task<List<ReviewViewModel>> GetReviewsByMovieAsync(int movieId)
    {
        try
        {
            var allReviews = await _coreReviewService.Get();
            var reviewsByMovieId = allReviews.Where(r => r.MovieId == movieId).ToList();
            return reviewsByMovieId.Select(r => Mapper.GenerateReviewAsViewModel(r)).ToList(); 
        }
        catch (Exception)
        {
            return new List<ReviewViewModel>();
        }
    }
}
