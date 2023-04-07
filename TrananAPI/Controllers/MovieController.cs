using TrananAPI.Data;
using TrananAPI.DTO;
using Microsoft.AspNetCore.Mvc; //kolla upp varför just mvc

namespace TrananAPI.Controllers;

[ApiController]
[Route("movie")]
public class MovieController : ControllerBase
{
    private readonly MovieRepository _repository;

    public MovieController(MovieRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies()
    {
        try
        {
            var movieDTOs = await _repository.GetMovies();
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
            var movieDTO = await _repository.GetMovieById(id);
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
            var newMovie = await _repository.CreateMovie(movieDTO);
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
            await _repository.UpdateMovie(movieDTO);
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
            await _repository.DeleteMovie(movieDTO);
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
            await _repository.DeleteMovies();
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }

 
}
