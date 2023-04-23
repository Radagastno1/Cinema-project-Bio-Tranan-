using TrananAPI.DTO;
using Core.Models;
using TrananAPI.Interface;
using Microsoft.AspNetCore.Mvc;
using TrananAPI.Service.Mapper;

namespace TrananAPI.Controllers;

[ApiController]
[Route("movie")]
public class MovieController : ControllerBase, IController<MovieDTO, MovieDTO>
{
    private readonly Core.Interface.IService<Movie> _coreMovieService;

    public MovieController(Core.Interface.IService<Movie> coreMovieService)
    {
        _coreMovieService = coreMovieService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDTO>>> Get()
    {
        try
        {
            var movies = await _coreMovieService.Get();
            if (movies == null)
            {
                return BadRequest("Failed to get movies.");
            }
            return Ok(movies.Select(m => Mapper.GenerateMovieDTO(m)));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MovieDTO>> GetById(int id)
    {
        try
        {
            var movie = await _coreMovieService.GetById(id);
            if (movie == null)
            {
                return BadRequest("Failed to get movie by id.");
            }
            return Mapper.GenerateMovieDTO(movie);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<MovieDTO>> Post(MovieDTO movieDTO)
    {
        try
        {
            var movie = Mapper.GenerateMovie(movieDTO);
            var newMovie = await _coreMovieService.Create(movie);
            var newMovieDTO = Mapper.GenerateMovieDTO(newMovie);
            if (newMovie == null)
            {
                return BadRequest("Failed to create movie.");
            }
            return CreatedAtAction(nameof(GetById), new { id = newMovieDTO.Id }, newMovieDTO);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<MovieDTO>> Put(MovieDTO movieDTO)
    {
        try
        {
            var movieToUpdate = Mapper.GenerateMovie(movieDTO);
            var updatedMovie = await _coreMovieService.Update(movieToUpdate);
            if (updatedMovie == null)
            {
                return BadRequest("Failed to update movie.");
            }
            return Ok(Mapper.GenerateMovieDTO(updatedMovie));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteById(int id)
    {
        try
        {
            await _coreMovieService.DeleteById(id);
            return StatusCode(204);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}
