using TrananAPI.DTO;
using TrananAPI.Interface;
using Microsoft.AspNetCore.Mvc;

namespace TrananAPI.Controllers;

[ApiController]
[Route("movie")]
public class MovieController : ControllerBase, IController<MovieDTO, MovieDTO>
{
    private readonly IService<MovieDTO, MovieDTO> _movieService;

    public MovieController(IService<MovieDTO, MovieDTO> movieService)
    {
        _movieService = movieService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDTO>>> Get()
    {
        try
        {
            var movieDTOs = await _movieService.Get();
            if(movieDTOs == null)
            {
                return BadRequest("Failed to get movies.");
            }
            return Ok(movieDTOs);
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
            var movieDTO = await _movieService.GetById(id);
            if(movieDTO == null)
            {
                return BadRequest("Failed to get movie by id.");
            }
            return Ok(movieDTO);
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
            var newMovie = await _movieService.Create(movieDTO);
            if(newMovie == null)
            {
                return BadRequest("Failed to create movie.");
            }
            return CreatedAtAction(nameof(GetById), new { id = newMovie.Id }, newMovie);
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
            var updatedMovie = await _movieService.Update(movieDTO);
            if(updatedMovie == null)
            {
                return BadRequest("Failed to update movie.");
            }
            return Ok(updatedMovie);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    // [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteById(int id)
    {
        try
        {
            await _movieService.DeleteById(id);
            return StatusCode(204);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}
