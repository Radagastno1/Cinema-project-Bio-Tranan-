using TrananMVC.ViewModel;

namespace TrananMVC.Interface;

public interface IReviewService
{
    public Task<ReviewViewModel> CreateReview(ReviewViewModel reviewViewModel);
    public Task<List<ReviewViewModel>> GetReviewsByMovieAsync(int movieId);
}
