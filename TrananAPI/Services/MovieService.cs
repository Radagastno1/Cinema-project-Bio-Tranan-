using Core.Models;
using TrananAPI.Interface;
using TrananAPI.DTO;
using TrananAPI.Service.Mapper;

namespace TrananAPI.Services;

public class MovieService : IService<MovieDTO, MovieDTO>
{
    private readonly Core.Interface.IService<Movie> _coreMovieService;

    public MovieService(Core.Interface.IService<Movie> coreMovieService)
    {
        _coreMovieService = coreMovieService;
    }

    public async Task<IEnumerable<MovieDTO>> Get()
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

    public async Task<MovieDTO> GetById(int id)
    {
        try
        {
            var movieFound = await _coreMovieService.GetById(id);
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

    public async Task<MovieDTO> Create(MovieDTO movieDTO)
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

    public async Task<MovieDTO> Update(MovieDTO movieDTO)
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

    public async Task DeleteById(int id)
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
