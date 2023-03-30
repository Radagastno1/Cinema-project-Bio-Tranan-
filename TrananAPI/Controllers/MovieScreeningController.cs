using TrananAPI.Data;
using TrananAPI.Models;
using TrananAPI.Models.DTO;
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

    // [HttpGet("{id:int}")]
    // public async Task<ActionResult<MovieDTO>> GetMovieById(int id)
    // {
    //     try
    //     {
    //         var movie = await _seedData.GetMovieById(id);
    //         return Ok(GenerateMovieDTO(movie));
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e);
    //         return NotFound();
    //     }
    // }

    // [HttpPost]
    // public async Task<ActionResult<MovieDTO>> PostMovie(Movie movie)
    // {
    //     try
    //     {
    //         var newMovie = await _seedData.CreateMovie(movie);
    //         return Ok(GenerateMovieDTO(newMovie));
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e);
    //         return NotFound();
    //     }
    // }

    // [HttpPut]
    // public async Task<ActionResult> PutMovie(Movie movie)
    // {
    //     try
    //     {
    //         await _seedData.UpdateMovie(movie);
    //         return Ok();
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e);
    //         return NotFound();
    //     }
    // }

    // [HttpDelete]
    // public async Task<ActionResult> DeleteMovie(Movie movie)
    // {
    //     try
    //     {
    //         await _seedData.DeleteMovie(movie);
    //         return Ok();
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e);
    //         return NotFound();
    //     }
    // }

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

    // private MovieDTO GenerateMovieDTO(Movie movie)
    // {
    //     var movieDTO = new MovieDTO(
    //         movie.MovieId,
    //         movie.Title,
    //         movie.ReleaseYear,
    //         movie.Language,
    //         movie.AmountOfScreenings,
    //         movie.MaxScreenings,
    //         movie.DurationSeconds,
    //         movie.Actors.Select(actor => $"{actor.FirstName} {actor.LastName}").ToList()
    //     );
    //     return movieDTO;
    // }
}
