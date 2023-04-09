using TrananAPI.Service;
using TrananAPI.DTO;
using Microsoft.AspNetCore.Mvc; //kolla upp varför just mvc

namespace TrananAPI.Controllers;

[ApiController]
[Route("moviescreening")]
public class MovieScreeningController : ControllerBase
{
    private readonly MovieScreeningService _movieScreeningService;

    public MovieScreeningController(MovieScreeningService movieScreeningService)
    {
        _movieScreeningService = movieScreeningService;
    }

    [HttpGet]
    public async Task<ActionResult<List<MovieScreeningOutgoingDTO>>> GetMovieScreenings()
    {
        try
        {
            var movieScreenings =
                await _movieScreeningService.GetUpcomingScreenings()
                ?? new List<MovieScreeningOutgoingDTO>();
            return Ok(movieScreenings);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MovieScreeningOutgoingDTO>> GetMovieScreeningById(int id)
    {
        try
        {
            var movieScreeningDTO = await _movieScreeningService.GetMovieScreeningById(id);
            return Ok(movieScreeningDTO);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult<MovieScreeningOutgoingDTO>> PostMovieScreeningDTO(
        MovieScreeningIncomingDTO movieScreeningDTO
    )
    {
        try
        {
            var newMovieScreening = await _movieScreeningService.CreateMovieScreening(
                movieScreeningDTO
            );
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
    }

    [HttpPut]
    public async Task<ActionResult> PutMovieScreening(MovieScreeningIncomingDTO movieScreeningDTO)
    {
        try
        {
            await _movieScreeningService.UpdateMovieScreening(movieScreeningDTO);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteMovieScreening(int id)
    {
        try
        {
            await _movieScreeningService.DeleteMovieScreeningById(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }
}
