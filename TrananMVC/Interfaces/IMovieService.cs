using TrananMVC.ViewModel;

namespace TrananMVC.Interface;

public interface IMovieService
{
     public Task<List<MovieViewModel>> GetUpcomingMoviesAsync();

    public Task<List<MovieViewModel>> GetAllMoviesAsync();

    public Task<MovieViewModel> GetMovieByIdAsync(int movieId);
}