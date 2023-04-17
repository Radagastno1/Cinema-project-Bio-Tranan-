using Core.Interface;
using Core.Models;
using TrananMVC.ViewModel;
using TrananMVC.Interface;

namespace TrananMVC.Service;

public class MovieService : IMovieService
{
    private readonly IService<Movie> _coreMovieService;
    private readonly IMovieTrailerService _movieTrailerService;
    private readonly ReviewService _reviewService;

    public MovieService(IService<Movie> coreMovieService, IMovieTrailerService movieTrailerService,ReviewService reviewService)
    {
        _coreMovieService = coreMovieService;
        _movieTrailerService = movieTrailerService;
        _reviewService = reviewService;
    }
    public async Task<List<MovieViewModel>> GetUpcomingMoviesAsync()
    {
        try
        {
            var movies = await _coreMovieService.Get();
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
            var movies = await _coreMovieService.Get();
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
            var movie = await _coreMovieService.GetById(movieId);
            var movieViewModel = Mapper.GenerateMovieAsViewModel(movie);
            movieViewModel.Reviews = await _reviewService.GetReviewsByMovieAsync(movieId) ?? null;
            movieViewModel.TrailerLink =
                await _movieTrailerService.GetYoutubeTrailerLinkByMovieId(movie) ?? null;
            return movieViewModel;
        }
        catch (Exception)
        {
            return new MovieViewModel();
        }
    }
}
