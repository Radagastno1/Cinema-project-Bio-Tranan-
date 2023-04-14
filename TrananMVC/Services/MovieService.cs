using Core.Services;
using TrananMVC.ViewModel;

namespace TrananMVC.Service;

public class MovieService
{
    private readonly MovieCoreService _movieCoreService;

    public MovieService(MovieCoreService movieCoreService)
    {
        _movieCoreService = movieCoreService;
    }
      public async Task<List<MovieViewModel>> GetUpcomingMovies()
    {
        try
        {
            var movies = await _movieCoreService.GetAllMovies();
            var upcomingMovies = movies.Where(m => m.AmountOfScreenings < m.MaxScreenings);
            var moviesAsViewModels = movies.Select(m => Mapper.GenerateMovieAsViewModel(m)).ToList();
            return moviesAsViewModels;
        }
        catch(Exception)
        {
            return new List<MovieViewModel>();
        }
    }

    public async Task<List<MovieViewModel>> GetAllMovies()
    {
        try
        {
            var movies = await _movieCoreService.GetAllMovies();
            return movies.Select(m => Mapper.GenerateMovieAsViewModel(m)).ToList();
        }
        catch (Exception)
        {
            return new List<MovieViewModel>();
        }
    }

    public async Task<MovieViewModel> GetMovieById(int movieId)
    {
        try
        {
            var movie = await _movieCoreService.GetMovieById(movieId);
            return Mapper.GenerateMovieAsViewModel(movie);
        }
        catch (Exception)
        {
            return new MovieViewModel();
        }
    }
}
