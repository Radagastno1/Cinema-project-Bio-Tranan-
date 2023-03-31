using TrananAPI.Data;
using TrananAPI.Models;
using TrananAPI.DTO;
using Microsoft.AspNetCore.Mvc; //kolla upp varför just mvc

namespace TrananAPI.Controllers;

[ApiController]
[Route("moviescreening")]
public class MovieScreeningController : ControllerBase
{
    private readonly MovieScreeningSeedData _seedData;

    public MovieScreeningController(MovieScreeningSeedData seedData)
    {
        _seedData = seedData;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieScreening>>> GetMovieScreenings()
    {
        try
        {
            var movieScreenings = await _seedData.GetScreenings();
            // var movieDTOs = movies.Select(m => GenerateMovieDTO(m)).ToList();
            return Ok(movieScreenings);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MovieScreeningDTO>> GetMovieScreeningById(int id)
    {
        try
        {
            var movieScreeningDTO = await _seedData.GetMovieScreeningById(id);
            return Ok(movieScreeningDTO);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult<MovieScreeningDTO>> PostMovieScreeningDTO(MovieScreeningDTO movieScreeningDTO)
    {
        try
        {
            var newMovieScreening = await _seedData.CreateMovieScreening(movieScreeningDTO);
            return Ok(newMovieScreening);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }

    [HttpPut]
    public async Task<ActionResult> PutMovieScreening(MovieScreeningDTO movieScreeningDTO)
    {
        try
        {
            await _seedData.UpdateMovieScreening(movieScreeningDTO);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteMovieScreening(MovieScreeningDTO movieScreeningDTO)
    {
        try
        {
            await _seedData.DeleteMovieScreening(movieScreeningDTO);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }

    // [HttpDelete("delete-all")]
    // public async Task<ActionResult> DeleteMovies()
    // {
    //     try
    //     {
    //         await _seedData.DeleteMovies();
    //         return Ok();
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e);
    //         return NotFound();
    //     }
    // }

    // private MovieScreeningDTO GenerateMovieScreeningDTO(MovieScreening movieScreening)
    // {
    //     var movieScreeningDTO = new MovieScreeningDTO()
    //     {
    //         MovieScreeningId = movieScreening.MovieScreeningId,
    //         DateAndTime = movieScreening.DateAndTime,
    //         MovieDTO = Mapper.GenerateMovieDTO(movieScreening.Movie)  
    //     };
    //     return movieScreeningDTO;
    // }
}
