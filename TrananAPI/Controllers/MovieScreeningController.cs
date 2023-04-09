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
            var movieScreenings = await _movieScreeningService.GetUpcomingScreenings() ?? new List<MovieScreeningOutgoingDTO>();
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
    public async Task<ActionResult<MovieScreeningOutgoingDTO>> PostMovieScreeningDTO(MovieScreeningIncomingDTO movieScreeningDTO)
    {
        try
        {
            var newMovieScreening = await _movieScreeningService.CreateMovieScreening(movieScreeningDTO);
            return Ok(newMovieScreening);
        }
        catch (NullReferenceException)
        {
            return BadRequest("Invalid input data.");
        }
        catch(InvalidOperationException)
        {
            return BadRequest("Maximum amount of viewings for this movie has been reached.");
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
