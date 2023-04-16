using Core.Services;
using TrananMVC.ViewModel;
using TrananMVC.Interface;

namespace TrananMVC.Service;

public class ReviewService 
{
    private readonly ReviewCoreService _reviewCoreService;

    public ReviewService(ReviewCoreService reviewCoreService)
    {
        _reviewCoreService = reviewCoreService;
    }

    public async Task<ReviewViewModel> CreateReview(ReviewViewModel reviewViewModel)
    {
        var review = Mapper.GenerateReview(reviewViewModel);
        var createdReview = await _reviewCoreService.CreateReview(review);
        return Mapper.GenerateReviewAsViewModel(createdReview);
    }
    // public async Task<List<MovieViewModel>> GetAllReviewsAsync()
    // {
    //     try
    //     {
    //         var movies = await _movieCoreService.GetAllMovies();
    //         return movies.Select(m => Mapper.GenerateMovieAsViewModel(m)).ToList();
    //     }
    //     catch (Exception)
    //     {
    //         return new List<MovieViewModel>();
    //     }
    // }
}
