using TrananMVC.Models;
using TrananMVC.ViewModel;
using TrananMVC.Repository;

namespace TrananMVC.Service;

public class MovieService
{
    private readonly MovieRepository _movieRepository;

    public MovieService(MovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<List<MovieViewModel>> GetAllMovies()
    {
        try
        {
            var movies = await _movieRepository.GetMovies();
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
            var movie = await _movieRepository.GetMovieById(movieId);
            return Mapper.GenerateMovieAsViewModel(movie);
        }
        catch (Exception)
        {
            return new MovieViewModel();
        }
    }
}
