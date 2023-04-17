using TrananAPI.Services;
using TrananAPI.DTO;
using TrananAPI.Interface;
using Microsoft.AspNetCore.Mvc;

namespace TrananAPI.Controllers;

[ApiController]
[Route("moviescreening")]
public class MovieScreeningController : ControllerBase, IController<MovieScreeningOutgoingDTO, MovieScreeningIncomingDTO>
{
    private readonly MovieScreeningService _movieScreeningService;

    public MovieScreeningController(MovieScreeningService movieScreeningService)
    {
        _movieScreeningService = movieScreeningService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieScreeningOutgoingDTO>>> Get()
    {
        try
        {
            var movieScreenings = await _movieScreeningService.GetUpcomingScreenings();
            if (movieScreenings == null)
            {
                return BadRequest("Failed to get movie screenings.");
            }
            return Ok(movieScreenings);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MovieScreeningOutgoingDTO>> GetById(int id)
    {
        try
        {
            var movieScreeningDTO = await _movieScreeningService.GetMovieScreeningById(id);
            if (movieScreeningDTO == null)
            {
                return BadRequest("Failed to get movie screening by id.");
            }
            return Ok(movieScreeningDTO);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<MovieScreeningOutgoingDTO>> Post(
        MovieScreeningIncomingDTO movieScreeningDTO
    )
    {
        try
        {
            var newMovieScreening = await _movieScreeningService.CreateMovieScreening(
                movieScreeningDTO
            );
            if (newMovieScreening == null)
            {
                return BadRequest("Failed to create movie screening.");
            }
            return Ok(newMovieScreening);
        }
        catch (InvalidOperationException e)
        {
            if (e.Message == "Theater not available at chosen time and day.")
            {
                return BadRequest(e.Message);
            }
            else if (e.Message == "Movie has maximum amount moviescreenings.")
            {
                return BadRequest(e.Message);
            }
            return BadRequest(e.Message);
        }
        catch (NullReferenceException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<MovieScreeningOutgoingDTO>> Put(MovieScreeningIncomingDTO movieScreeningDTO)
    {
        try
        {
            var updatedMovieScreening = await _movieScreeningService.UpdateMovieScreening(
                movieScreeningDTO
            );
            if (updatedMovieScreening == null)
            {
                return BadRequest("Failed to update movie screening.");
            }
            return Ok(updatedMovieScreening);
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
            await _movieScreeningService.DeleteMovieScreeningById(id);
            return StatusCode(204);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}
