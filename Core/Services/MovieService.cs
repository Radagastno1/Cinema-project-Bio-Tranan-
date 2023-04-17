using Core.Interface;
using Core.Models;

namespace Core.Services;

public class MovieService : IService<Movie>
{
    private IRepository<Movie> _movieRepository;
    private IActorRepository _actorRepository;

    public MovieService(IRepository<Movie> movieRepository, IActorRepository actorRepository)
    {
        _movieRepository = movieRepository;
        _actorRepository = actorRepository;
    }

    public async Task<IEnumerable<Movie>> Get()
    {
        try
        {
            var allMovies = await _movieRepository.GetAsync();
            if (allMovies == null)
            {
                return new List<Movie>();
            }
            return allMovies;
        }
        catch (Exception)
        {
            throw new Exception("Failed to get movies.");
        }
    }

    public async Task<Movie> GetById(int movieId)
    {
        try
        {
            var movieFound = await _movieRepository.GetByIdAsync(movieId);
            if (movieFound == null)
            {
                return new Movie();
            }
            return movieFound;
        }
        catch (Exception)
        {
            throw new Exception("Failed to get movie by id.");
        }
    }

    public async Task<Movie> Create(Movie movie)
    {
        try
        {
            List<Actor> actorsOfMovie = new();
            foreach (var actor in movie.Actors)
            {
                var actorInDB = await _actorRepository.GetActorByIdAsync(actor.ActorId);
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
                var directorInDb = await _actorRepository.GetDirectorByIdAsync(director.DirectorId);
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
            var recentlyAddedMovie = await _movieRepository.CreateAsync(movie);
            return recentlyAddedMovie;
        }
        catch (Exception)
        {
            throw new Exception("Something went wrong when saving movie.");
        }
    }

    public async Task<Movie> Update(Movie movie)
    {
        try
        {
            var updatedMovie = await _movieRepository.UpdateAsync(movie);
            return updatedMovie;
        }
        catch (Exception)
        {
            throw new Exception("Failed when updating movies.");
        }
    }

    public async Task DeleteById(int id)
    {
        try
        {
            await _movieRepository.DeleteByIdAsync(id);
        }
        catch (Exception e)
        {
            throw new Exception("Failed when deleting movie by id.");
        }
    }
}
