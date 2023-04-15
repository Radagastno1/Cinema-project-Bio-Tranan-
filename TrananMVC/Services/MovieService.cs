using Core.Services;
using TrananMVC.ViewModel;
using TrananMVC.Interface;

namespace TrananMVC.Service;

public class MovieService : IMovieService
{
    private readonly MovieCoreService _movieCoreService;
    private readonly IMovieTrailerService _movieTrailerService;

    public MovieService(MovieCoreService movieCoreService, IMovieTrailerService movieTrailerService)
    {
        _movieCoreService = movieCoreService;
        _movieTrailerService = movieTrailerService;
    }
    public async Task<List<MovieViewModel>> GetUpcomingMoviesAsync()
    {
        try
        {
            var movies = await _movieCoreService.GetAllMovies();
            var upcomingMovies = movies.Where(m => m.AmountOfScreenings < m.MaxScreenings);
            var moviesAsViewModels = movies
                .Select(m => Mapper.GenerateMovieAsViewModel(m))
                .ToList();
            return moviesAsViewModels;
        }
        catch (Exception)
        {
            return new List<MovieViewModel>();
        }
    }

    public async Task<List<MovieViewModel>> GetAllMoviesAsync()
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

    public async Task<MovieViewModel> GetMovieByIdAsync(int movieId)
    {
        try
        {
            var movie = await _movieCoreService.GetMovieById(movieId);
            var movieViewModel = Mapper.GenerateMovieAsViewModel(movie);
            movieViewModel.TrailerLink =
                await _movieTrailerService.GetTrailerLinkByMovieId(movie) ?? null;
            return movieViewModel;
        }
        catch (Exception)
        {
            return new MovieViewModel();
        }
    }
}
