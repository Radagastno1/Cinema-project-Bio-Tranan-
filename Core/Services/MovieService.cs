using Core.Data.Repository;
using Core.Models;

namespace Core.Services;

public class MovieCoreService
{
    private MovieRepository _movieRepository;

    public MovieCoreService(MovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<IEnumerable<Movie>> GetAllMovies()
    {
        try
        {
            var allMovies = await _movieRepository.GetMovies();
            if (allMovies == null)
            {
                return new List<Movie>();
            }
            return allMovies;
        }
        catch (Exception)
        {
            return new List<Movie>();
        }
    }

    public async Task<Movie> GetMovieById(int movieId)
    {
        try
        {
            var movieFound = await _movieRepository.GetMovieById(movieId);
            if (movieFound == null)
            {
                return null;
            }
            return movieFound;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<Movie> CreateMovie(Movie movie)
    {
        try
        {
            List<Actor> actorsOfMovie = new();
            foreach (var actor in movie.Actors)
            {
                var actorInDB = await _movieRepository.GetActorById(actor.ActorId);
                if (actorInDB == null)
                {
                    actorsOfMovie.Add(actor);
                }
                else
                {
                    actorsOfMovie.Add(actorInDB);
                }
            }
            List<Director> directorsOfMovie = new();
            foreach (var director in movie.Directors)
            {
                var directorInDb = await _movieRepository.GetDirectorById(director.DirectorId);
                if (directorInDb == null)
                {
                    directorsOfMovie.Add(director);
                }
                else
                {
                    directorsOfMovie.Add(directorInDb);
                }
            }

            movie.Actors = actorsOfMovie;
            movie.Directors = directorsOfMovie;
            var recentlyAddedMovie = await _movieRepository.CreateMovie(movie);
            return recentlyAddedMovie;
        }
        catch (Exception)
        {
            throw new Exception();
        }
    }

    public async Task<Movie> UpdateMovie(Movie movie)
    {
        var updatedMovie = await _movieRepository.UpdateMovie(movie);
        return updatedMovie;
    }

    public async Task DeleteMovieById(int id)
    {
        await _movieRepository.DeleteMovieById(id);
    }

    public async Task DeleteMovies()
    {
        await _movieRepository.DeleteMovies();
    }
}
