using Core.Models;
using TrananMVC.ViewModel;
using TrananMVC.Interface;

namespace TrananMVC.Service;

public class MovieService : IMovieService<MovieViewModel>
{
    private readonly Core.Interface.IService<Movie> _coreMovieService;
    private readonly IMovieTrailerService _movieTrailerService;
    private readonly IReviewService _reviewService;

    public MovieService(
        Core.Interface.IService<Movie> coreMovieService,
        IMovieTrailerService movieTrailerService,
        IReviewService reviewService
    )
    {
        _coreMovieService = coreMovieService;
        _movieTrailerService = movieTrailerService;
        _reviewService = reviewService;
    }

    public async Task<List<MovieViewModel>> GetUpcoming()
    {
        try
        {
            var movies = await _coreMovieService.Get();
            if (movies == null)
            {
                return new List<MovieViewModel>();
            }
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

    public async Task<List<MovieViewModel>> GetAll()
    {
        try
        {
            var movies = await _coreMovieService.Get();
            if (movies == null)
            {
                return new List<MovieViewModel>();
            }
            return movies.Select(m => Mapper.GenerateMovieAsViewModel(m)).ToList();
        }
        catch (Exception)
        {
            return new List<MovieViewModel>();
        }
    }

    public async Task<MovieViewModel> GetById(int movieId)
    {
        try
        {
            var movie = await _coreMovieService.GetById(movieId);
            if (movie == null)
            {
                return new MovieViewModel();
            }
            var movieViewModel = Mapper.GenerateMovieAsViewModel(movie);
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
