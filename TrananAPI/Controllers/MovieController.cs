using TrananAPI.Services;
using TrananAPI.DTO;
using Microsoft.AspNetCore.Mvc; 

namespace TrananAPI.Controllers;

[ApiController]
[Route("movie")]
public class MovieController : ControllerBase
{
    private readonly MovieService _movieService;

    public MovieController(MovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies()
    {
        try
        {
            var movieDTOs = await _movieService.GetAllMoviesAsDTOs();
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
            var movieDTO = await _movieService.GetMovieAsDTOById(id);
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
            var newMovie = await _movieService.CreateMovie(movieDTO);
            return Ok(newMovie);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }

    [HttpPut]
    public async Task<ActionResult<MovieDTO>> PutMovie(MovieDTO movieDTO)
    {
        try
        {
            var updatedMovie = await _movieService.UpdateMovie(movieDTO);
            return Ok(updatedMovie);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteMovieById(int id)
    {
        try
        {
            await _movieService.DeleteMovieById(id);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }

    // [HttpDelete("delete-all")]
    // public async Task<IActionResult> DeleteMovies()
    // {
    //     try
    //     {
    //         await _movieService.DeleteMovies();
    //         return NoContent();
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e);
    //         return NotFound();
    //     }
    // }

 
}
