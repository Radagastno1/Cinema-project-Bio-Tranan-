using TrananAPI.Data;
using TrananAPI.Models;
using TrananAPI.Models.DTO;
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
            var movies = await _seedData.GetMovies();
            var movieDTOs = movies
                .Select(
                    m =>
                        new MovieDTO(
                            m.MovieId,
                            m.Title,
                            m.ReleaseYear,
                            m.Language,
                            m.AmountOfScreenings,
                            m.MaxScreenings,
                            m.DurationSeconds,
                            m.Actors
                                .Select(actor => $"{actor.FirstName} {actor.LastName}")
                                .ToList()
                        )
                )
                .ToList();
            return Ok(movieDTOs);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Movie>> GetMovieById(int id)
    {
        try
        {
            var movie = await _seedData.GetMovieById(id);
            return Ok(movie);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult<Movie>> PostMovie(Movie movie)
    {
        try
        {
            var newMovie = await _seedData.CreateMovie(movie);
            return Ok(newMovie);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }

    [HttpPut]
    public async Task<ActionResult> PutMovie(Movie movie)
    {
        try
        {
            await _seedData.UpdateMovie(movie);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteMovie(Movie movie)
    {
        try
        {
            await _seedData.DeleteMovie(movie);
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
