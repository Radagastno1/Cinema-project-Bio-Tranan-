using TrananAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace TrananAPI.Data;

public class TheaterSeedData
{
    private readonly TrananDbContext _trananDbContext;

    public TheaterSeedData(TrananDbContext trananDbContext)
    {
        _trananDbContext = trananDbContext;
    }

    public async Task<List<Theater>> GetTheaters()
    {
        try
        {
            if (_trananDbContext.Theaters.Count() < 1)
            {
                await _trananDbContext.Theaters.AddAsync(GenerateRandomTheater());
                await _trananDbContext.SaveChangesAsync();
            }
            return await _trananDbContext.Theaters.ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return null;
    }

    // public async Task<Theater> GetTheaterById(int id)
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

    // public async Task<Theater> CreateTheater(Movie movie)
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

    // public async Task UpdateTheater(Theater theater)
    // {
    //     try
    //     {
    //         // var movieToUpdate = await _trananDbContext.Movies.FindAsync(movie.MovieId);
    //         // movieToUpdate = movie;
    //         _trananDbContext.Update(movie);
    //         await _trananDbContext.SaveChangesAsync();
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e.Message);
    //     }
    // }

    // public async Task AddTheater(Theater theater)
    // {
    //     await _trananDbContext.Movies.AddAsync(movie);
    //     await _trananDbContext.SaveChangesAsync();
    // }

    // public async Task DeleteTheater(Theater theater)
    // {
    //     _trananDbContext.Movies.Remove(movie);
    //     await _trananDbContext.SaveChangesAsync();
    // }
    private Theater GenerateRandomTheater()
    {
        var seats = new List<Seat>()
        {
            new Seat(1, 1),
            new Seat(1, 2),
            new Seat(1, 3),
            new Seat(1, 4),
            new Seat(1, 5)
        };
        var theater = new Theater("Tranan123", seats);
        return theater;
    }
}
