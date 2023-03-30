using Microsoft.EntityFrameworkCore;
using TrananAPI.Models;

namespace TrananAPI.Data;

public class SeedData
{
    private readonly TrananDbContext _trananDbContext;

    public SeedData(TrananDbContext trananDbContext)
    {
        _trananDbContext = trananDbContext;
    }

    public async Task<List<Movie>> GetMovies()
    {
        try
        {
            var movies = await _trananDbContext.Movies.ToListAsync();
            if (movies == null)
            {
                movies = new List<Movie>()
                {
                    new Movie("Harry Potter", 2023, "English", 10, 12),
                    new Movie("StarGaze", 2023, "English", 2, 15),
                    new Movie("Påskfilmen", 2023, "Svenska", 8, 10)
                };
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            var movies = new List<Movie>()
            {
                new Movie("Harry Potter", 2023, "English", 10, 12),
                new Movie("StarGaze", 2023, "English", 2, 15),
                new Movie("Påskfilmen", 2023, "Svenska", 8, 10)
            };
            return movies;
        }
        return null;
    }
}
