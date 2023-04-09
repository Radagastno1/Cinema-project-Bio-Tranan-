using TrananAPI.Data.Repository;
using TrananAPI.DTO;
using TrananAPI.Models;

namespace TrananAPI.Service;

public class MovieService
{
    private MovieRepository _movieRepository;

    public MovieService(MovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<IEnumerable<MovieDTO>> GetAllMoviesAsDTOs()
    {
        try
        {
            var allMovies = await _movieRepository.GetMovies();
            if (allMovies == null)
            {
                return new List<MovieDTO>();
            }
            var allMoviesAsDtos = allMovies.Select(m => Mapper.GenerateMovieDTO(m));
            return allMoviesAsDtos;
        }
        catch (Exception)
        {
            return new List<MovieDTO>();
        }
    }

    public async Task<MovieDTO> GetMovieAsDTOById(int movieId)
    {
        try
        {
            var movieFound = await _movieRepository.GetMovieById(movieId);
            if (movieFound == null)
            {
                return null;
            }
            return Mapper.GenerateMovieDTO(movieFound);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<MovieDTO> CreateMovie(MovieDTO movieDTO)
    {
        try
        {
            List<Actor> actorsOfMovie = new();
            foreach (var actor in movieDTO.ActorDTOs)
            {
                var actorInDB = await _movieRepository.GetActorById(actor.ActorId);
                if (actorInDB == null)
                {
                    var newActor = Mapper.GenerateActor(actor);
                    actorsOfMovie.Add(newActor);
                }
                else
                {
                    actorsOfMovie.Add(actorInDB);
                }
            }
            List<Director> directorsOfMovie = new();
            foreach (var director in movieDTO.DirectorDTOs)
            {
                var directorInDb = await _movieRepository.GetDirectorById(director.DirectorId);
                if (directorInDb == null)
                {
                    var newDirector = Mapper.GenerateDirector(director);
                    directorsOfMovie.Add(newDirector);
                }
                else
                {
                    directorsOfMovie.Add(directorInDb);
                }
            }
           
            var newMovie = Mapper.GenerateMovie(movieDTO);
            newMovie.Actors = actorsOfMovie;
            newMovie.Directors = directorsOfMovie;
            var recentlyAddedMovie = await _movieRepository.CreateMovie(newMovie);
            return Mapper.GenerateMovieDTO(newMovie);
        }
        catch (Exception)
        {
            throw new Exception();
        }
    }

    public async Task<MovieDTO> UpdateMovie(MovieDTO movieDTO)
    {
        var movieToUpdate = Mapper.GenerateMovie(movieDTO);
        var updatedMovie = await _movieRepository.UpdateMovie(movieToUpdate);
        return Mapper.GenerateMovieDTO(updatedMovie);
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
