using Core.Models;
using Core.Services;
using TrananAPI.DTO;
using TrananAPI.Service;

namespace TrananAPI.Services;

public class MovieService
{
    private readonly MovieCoreService _movieCoreService;

    public MovieService(MovieCoreService movieCoreService)
    {
        _movieCoreService = movieCoreService;
    }

    public async Task<IEnumerable<MovieDTO>> GetAllMoviesAsDTOs()
    {
        try
        {
            var allMovies = await _movieCoreService.GetAllMovies();
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
            var movieFound = await _movieCoreService.GetMovieById(movieId);
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
            var movie = Mapper.GenerateMovie(movieDTO);
            var createdMovie = await _movieCoreService.CreateMovie(movie);
            return Mapper.GenerateMovieDTO(movie);
            // List<Actor> actorsOfMovie = new();
            // foreach (var actor in movieDTO.ActorDTOs)
            // {
            //     var actorInDB = await _movieCoreService.GetActorById(actor.ActorId);
            //     if (actorInDB == null)
            //     {
            //         var newActor = Mapper.GenerateActor(actor);
            //         actorsOfMovie.Add(newActor);
            //     }
            //     else
            //     {
            //         actorsOfMovie.Add(actorInDB);
            //     }
            // }
            // List<Director> directorsOfMovie = new();
            // foreach (var director in movieDTO.DirectorDTOs)
            // {
            //     var directorInDb = await _movieCoreService.GetDirectorById(director.DirectorId);
            //     if (directorInDb == null)
            //     {
            //         var newDirector = Mapper.GenerateDirector(director);
            //         directorsOfMovie.Add(newDirector);
            //     }
            //     else
            //     {
            //         directorsOfMovie.Add(directorInDb);
            //     }
            // }
           
            // var newMovie = Mapper.GenerateMovie(movieDTO);
            // newMovie.Actors = actorsOfMovie;
            // newMovie.Directors = directorsOfMovie;
            // var recentlyAddedMovie = await _movieCoreService.CreateMovie(newMovie);
            // return Mapper.GenerateMovieDTO(newMovie);
        }
        catch (Exception)
        {
            throw new Exception();
        }
    }

    public async Task<MovieDTO> UpdateMovie(MovieDTO movieDTO)
    {
        var movieToUpdate = Mapper.GenerateMovie(movieDTO);
        var updatedMovie = await _movieCoreService.UpdateMovie(movieToUpdate);
        return Mapper.GenerateMovieDTO(updatedMovie);
    }
    public async Task DeleteMovieById(int id)
    {
        await _movieCoreService.DeleteMovieById(id);
    }
    public async Task DeleteMovies()
    {
        await _movieCoreService.DeleteMovies();
    }

}
