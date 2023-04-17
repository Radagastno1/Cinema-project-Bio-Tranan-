using Core.Models;
using Core.Interface;
using TrananAPI.DTO;
using TrananAPI.Service;

namespace TrananAPI.Services;

public class MovieService
{
    private readonly IService<Movie> _coreMovieService;

    public MovieService(IService<Movie> coreMovieService)
    {
        _coreMovieService = coreMovieService;
    }

    public async Task<IEnumerable<MovieDTO>> GetAllMoviesAsDTOs()
    {
        try
        {
            var allMovies = await _coreMovieService.Get();
            if (allMovies == null)
            {
                return new List<MovieDTO>();
            }
            var allMoviesAsDtos = allMovies.Select(m => Mapper.GenerateMovieDTO(m));
            return allMoviesAsDtos;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<MovieDTO> GetMovieAsDTOById(int movieId)
    {
        try
        {
            var movieFound = await _coreMovieService.GetById(movieId);
            if (movieFound == null)
            {
                return null;
            }
            return Mapper.GenerateMovieDTO(movieFound);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<MovieDTO> CreateMovie(MovieDTO movieDTO)
    {
        try
        {
            var movie = Mapper.GenerateMovie(movieDTO);
            var createdMovie = await _coreMovieService.Create(movie);
            return Mapper.GenerateMovieDTO(movie);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<MovieDTO> UpdateMovie(MovieDTO movieDTO)
    {
        try
        {
            var movieToUpdate = Mapper.GenerateMovie(movieDTO);
            var updatedMovie = await _coreMovieService.Update(movieToUpdate);
            return Mapper.GenerateMovieDTO(updatedMovie);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task DeleteMovieById(int id)
    {
        try
        {
            await _coreMovieService.DeleteById(id);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}
