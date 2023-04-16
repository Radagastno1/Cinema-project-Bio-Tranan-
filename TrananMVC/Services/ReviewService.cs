using Core.Services;
using TrananMVC.ViewModel;
using TrananMVC.Interface;

namespace TrananMVC.Service;

public class ReviewService 
{
    private readonly ReviewCoreService _reviewCoreService;
    private readonly ReservationCoreService _reservationCoreService;

    public ReviewService(ReviewCoreService reviewCoreService, ReservationCoreService reservationCoreService)
    {
        _reviewCoreService = reviewCoreService;
        _reservationCoreService = reservationCoreService;
    }

    public async Task<ReviewViewModel> CreateReview(ReviewViewModel reviewViewModel)
    {
        var reservation = await _reservationCoreService.GetReservationByReservationCode(reviewViewModel.ReservationCode);
        var review = Mapper.GenerateReview(reviewViewModel, reservation);
        var createdReview = await _reviewCoreService.CreateReview(review);
        return Mapper.GenerateReviewAsViewModel(createdReview);
    }
    public async Task<List<ReviewViewModel>> GetReviewsByMovieAsync(int movieId)
    {
        try
        {
            var allReviews = await _reviewCoreService.GetAllReviews();
            var reviewsByMovieId = allReviews.Where(r => r.MovieId == movieId).ToList();
            return reviewsByMovieId.Select(r => Mapper.GenerateReviewAsViewModel(r)).ToList(); 
        }
        catch (Exception)
        {
            return new List<ReviewViewModel>();
        }
    }
}
