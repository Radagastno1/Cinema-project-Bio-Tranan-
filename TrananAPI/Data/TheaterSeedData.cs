using TrananAPI.Models;
namespace TrananAPI.Data;

public class TheaterSeedData
{
    private readonly TrananDbContext _trananDbContext;

    public TheaterSeedData(TrananDbContext trananDbContext)
    {
        _trananDbContext = trananDbContext;
    }

    // public async Task<List<Theater>> GetTheaters()
    // {
    //     try
    //     {
    //         if (_trananDbContext.Movies.Count() < 1)
    //         {
    //             await _trananDbContext.AddRangeAsync(movies);
    //             await _trananDbContext.SaveChangesAsync();
    //         }
    //         return await _trananDbContext.Movies.ToListAsync() ?? new List<Movie>();
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e.Message);
    //     }
    //     return null;
    // }

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
}
