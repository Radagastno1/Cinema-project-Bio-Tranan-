using Microsoft.EntityFrameworkCore;
using TrananAPI.Models;

namespace TrananAPI.Data;

public class MovieSeedData
{
    private readonly TrananDbContext _trananDbContext;

    public MovieSeedData(TrananDbContext trananDbContext)
    {
        _trananDbContext = trananDbContext;
    }

    public async Task<List<Movie>> GetMovies()
    {
        try
        {
            if (_trananDbContext.Movies.Count() < 1)
            {
                var actors = new List<Actor>()
                {
                    new Actor("Isabella", "Tortellini"),
                    new Actor("Henrik", "Byström")
                };
                await _trananDbContext.AddAsync(
                    new Movie(1, "Harry Potter", 2023, "English", 10, 12, actors)
                );
                await _trananDbContext.SaveChangesAsync();
            }
            return await _trananDbContext.Movies.ToListAsync() ?? new List<Movie>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return null;
    }

    public async Task<Movie> GetMovieById(int id)
    {
        try
        {
            return await _trananDbContext.Movies.FindAsync(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<Movie> CreateMovie(Movie movie)
    {
        try
        {
            await _trananDbContext.AddAsync(movie);
            await _trananDbContext.SaveChangesAsync();
            return movie;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task UpdateMovie(Movie movie)
    {
        try
        {
            // var movieToUpdate = await _trananDbContext.Movies.FindAsync(movie.MovieId);
            // movieToUpdate = movie;
            _trananDbContext.Update(movie);
            await _trananDbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public async Task AddMovie(Movie movie)
    {
        await _trananDbContext.Movies.AddAsync(movie);
        await _trananDbContext.SaveChangesAsync();
    }

    public async Task DeleteMovie(Movie movie)
    {
        _trananDbContext.Movies.Remove(movie);
        await _trananDbContext.SaveChangesAsync();
    }

    public async Task DeleteMovies()
    {
        _trananDbContext.Movies.ToList().ForEach(m => _trananDbContext.Movies.Remove(m));
        await _trananDbContext.SaveChangesAsync();
    }
}
