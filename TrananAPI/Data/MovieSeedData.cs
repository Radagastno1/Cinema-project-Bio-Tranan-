using Microsoft.EntityFrameworkCore;
using TrananAPI.DTO;
using TrananAPI.Models;

namespace TrananAPI.Data;

public class MovieSeedData
{
    private readonly TrananDbContext _trananDbContext;

    public MovieSeedData(TrananDbContext trananDbContext)
    {
        _trananDbContext = trananDbContext;
    }

    public async Task<List<MovieDTO>> GetMovies()
    {
        try
        {
            if (_trananDbContext.Movies.Count() < 1)
            {
                await _trananDbContext.Movies.AddRangeAsync(GenerateRandomMovies());
                await _trananDbContext.SaveChangesAsync();
            }
            return await _trananDbContext.Movies
                    .Include(m => m.Actors)
                    .Include(m => m.Directors)
                    .Select(m => Mapper.GenerateMovieDTO(m)).ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return null;
    }

    public async Task<MovieDTO> GetMovieById(int id)
    {
        try //include
        {
            var movie = await _trananDbContext.Movies.FindAsync(id);
            return Mapper.GenerateMovieDTO(movie);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<MovieDTO> CreateMovie(MovieDTO movieDTO)
    {
        try
        {
            await _trananDbContext.AddAsync(Mapper.GenerateMovie(movieDTO));
            await _trananDbContext.SaveChangesAsync();
            return movieDTO;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task UpdateMovie(MovieDTO movieDTO)
    {
        try
        {
            _trananDbContext.Update(Mapper.GenerateMovie(movieDTO));
            await _trananDbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public async Task<MovieDTO> AddMovie(MovieDTO movieDTO)
    {
        await _trananDbContext.Movies.AddAsync(Mapper.GenerateMovie(movieDTO));
        await _trananDbContext.SaveChangesAsync();
        return movieDTO;
    }

    public async Task DeleteMovie(MovieDTO movieDTO)
    {
        _trananDbContext.Movies.Remove(Mapper.GenerateMovie(movieDTO));
        await _trananDbContext.SaveChangesAsync();
    }

    public async Task DeleteMovies()
    {
        _trananDbContext.Movies.ToList().ForEach(m => _trananDbContext.Movies.Remove(m));
        await _trananDbContext.SaveChangesAsync();
    }

    private List<Movie> GenerateRandomMovies()
    {
        var actors = new List<Actor>()
        {
            new Actor("Isabella", "Tortellini"),
            new Actor("Henrik", "Byström")
        };
        List<Movie> movies =
            new()
            {
                new Movie(1, "Harry Potter", 2023, "English", 208, actors),
                new Movie(2, "Kalle Anka", 2023, "English", 208, actors),
                new Movie(3, "Sagan om de sju", 2023, "English", 208, actors),
                new Movie(4, "Milkshake", 2023, "English", 208, actors),
                new Movie(5, "Macarena", 2023, "English", 208, actors),
            };

        return movies;
    }
}
