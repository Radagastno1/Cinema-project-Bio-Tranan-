using Microsoft.EntityFrameworkCore;
using TrananAPI.Models.DTO;
using TrananAPI.Models;

namespace TrananAPI.Data;

public class MovieScreeningSeedData
{
    private readonly TrananDbContext _trananDbContext;

    public MovieScreeningSeedData(TrananDbContext trananDbContext)
    {
        _trananDbContext = trananDbContext;
    }

    public async Task<List<MovieScreening>> GetScreenings()
    {
        try
        {
            if (_trananDbContext.MovieScreenings.Count() < 1)
            {
                await _trananDbContext.MovieScreenings.AddAsync(GenerateRandomMovieScreening());
                await _trananDbContext.SaveChangesAsync();
            }
            return await _trananDbContext.MovieScreenings.Include(m => m.Movie).Include(m => m.Theater).ToListAsync() ?? new List<MovieScreening>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return null;
    }

    // public async Task<Movie> GetMovieById(int id)
    // {
    //     try
    //     {
    //         return await _trananDbContext.Movies.FindAsync(id);
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e.Message);
    //         return null;
    //     }
    // }

    // public async Task<Movie> CreateMovie(Movie movie)
    // {
    //     try
    //     {
    //         await _trananDbContext.AddAsync(movie);
    //         await _trananDbContext.SaveChangesAsync();
    //         return movie;
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e.Message);
    //         return null;
    //     }
    // }

    // public async Task UpdateMovie(Movie movie)
    // {
    //     try
    //     {
    //         _trananDbContext.Update(movie);
    //         await _trananDbContext.SaveChangesAsync();
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e.Message);
    //     }
    // }

    // public async Task AddMovie(Movie movie)
    // {
    //     await _trananDbContext.Movies.AddAsync(movie);
    //     await _trananDbContext.SaveChangesAsync();
    // }

    // public async Task DeleteMovie(Movie movie)
    // {
    //     _trananDbContext.Movies.Remove(movie);
    //     await _trananDbContext.SaveChangesAsync();
    // }

    // public async Task DeleteMovies()
    // {
    //     _trananDbContext.Movies.ToList().ForEach(m => _trananDbContext.Movies.Remove(m));
    //     await _trananDbContext.SaveChangesAsync();
    // }
    private MovieScreeningDTO GenerateMovieScreeningDTO()
    {
        return new MovieScreeningDTO();
    }

    private MovieScreening GenerateRandomMovieScreening()
    {
       List<Seat>seats = new()
       {
        new Seat(1, 2),
        new Seat(2, 2),
        new Seat(3, 2),
        new Seat(4, 2)
       };
       List<Actor>actors = new()
       {
        new Actor("Beyonce", "Hawking")
       };

       Movie movie = new()
       {
        Title = "Bella Svan Prinsessan",
        Actors = actors,
        DurationSeconds = 123,
        Language = "svenska"
       };
       Theater theater = new()
       {
        Name = "Stora salen",
        Seats = seats

       };
        var movieScreening = new MovieScreening(DateTime.Now, movie, theater);

        return movieScreening;
    }
}