using TrananAPI.Data;
// using TrananAPI.Models;
using TrananAPI.DTO;
using Microsoft.AspNetCore.Mvc; //kolla upp varför just mvc

namespace TrananAPI.Controllers;

[ApiController]
[Route("movie")]
public class MovieController : ControllerBase
{
    private readonly MovieSeedData _seedData;

    public MovieController(MovieSeedData seedData)
    {
        _seedData = seedData;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies()
    {
        try
        {
            var movieDTOs = await _seedData.GetMovies();
            return Ok(movieDTOs);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MovieDTO>> GetMovieById(int id)
    {
        try
        {
            var movieDTO = await _seedData.GetMovieById(id);
            return Ok(movieDTO);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult<MovieDTO>> PostMovie(MovieDTO movieDTO)
    {
        try
        {
            var newMovie = await _seedData.CreateMovie(movieDTO);
            return Ok(newMovie);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }

    [HttpPut]
    public async Task<ActionResult> PutMovie(MovieDTO movieDTO)
    {
        try
        {
            await _seedData.UpdateMovie(movieDTO);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteMovie(MovieDTO movieDTO)
    {
        try
        {
            await _seedData.DeleteMovie(movieDTO);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }

    [HttpDelete("delete-all")]
    public async Task<ActionResult> DeleteMovies()
    {
        try
        {
            await _seedData.DeleteMovies();
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }

 
}
