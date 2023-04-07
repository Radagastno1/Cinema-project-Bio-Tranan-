using TrananAPI.Data;
using TrananAPI.Models;
using TrananAPI.DTO;
using Microsoft.AspNetCore.Mvc; //kolla upp varför just mvc

namespace TrananAPI.Controllers;

[ApiController]
[Route("moviescreening")]
public class MovieScreeningController : ControllerBase
{
    private readonly MovieScreeningRepository _repository;

    public MovieScreeningController(MovieScreeningRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<List<MovieScreeningOutgoingDTO>>> GetMovieScreenings()
    {
        try
        {
            var movieScreenings = await _repository.GetUpcomingScreenings() ?? new List<MovieScreeningOutgoingDTO>();
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
            var movieScreeningDTO = await _repository.GetMovieScreeningById(id);
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
            var newMovieScreening = await _repository.CreateMovieScreening(movieScreeningDTO);
            return Ok(newMovieScreening);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }

    [HttpPut]
    public async Task<ActionResult> PutMovieScreening(MovieScreeningIncomingDTO movieScreeningDTO)
    {
        try
        {
            await _repository.UpdateMovieScreening(movieScreeningDTO);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteMovieScreening(MovieScreeningIncomingDTO movieScreeningDTO)
    {
        try
        {
            await _repository.DeleteMovieScreening(movieScreeningDTO);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }
}
