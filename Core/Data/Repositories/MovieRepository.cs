using Microsoft.EntityFrameworkCore;
using Core.Models;
using Core.Interface;

namespace Core.Data.Repository;

public class MovieRepository : IRepository<Movie>
{
    private readonly TrananDbContext _trananDbContext;

    public MovieRepository(TrananDbContext trananDbContext)
    {
        _trananDbContext = trananDbContext;
    }

    public async Task<List<Movie>> GetAsync()
    {
        try
        {
            if (_trananDbContext.Movies.Count() < 1)
            {
                return new List<Movie>();
            }
            return await _trananDbContext.Movies
                .Include(m => m.Actors)
                .Include(m => m.Directors)
                .Include(m => m.Reviews)
                .ToListAsync();
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<Movie> GetByIdAsync(int id)
    {
        try 
        {
                  var reviews = await _trananDbContext.Reviews.ToListAsync();
        foreach(var item in reviews)
        {
            _trananDbContext.Reviews.Remove(item);
            Console.WriteLine(item.Comment);
        }

            var movie = await _trananDbContext.Movies
                .Include(m => m.Actors)
                .Include(m => m.Directors)
                .FirstAsync(m => m.MovieId == id);

            return movie;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<Movie> CreateAsync(Movie movie)
    {
        try
        {
            await _trananDbContext.Movies.AddAsync(movie);
            await _trananDbContext.SaveChangesAsync();
            var recentlyAddedMovie = await _trananDbContext.Movies
                .OrderByDescending(m => m.MovieId)
                .FirstOrDefaultAsync();
            return recentlyAddedMovie;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<Movie> UpdateAsync(Movie movie)
    { 
        try
        {
            var movieToUpdate = await _trananDbContext.Movies
                .Include(m => m.Actors)
                .Include(m => m.Directors)
                .FirstAsync(m => m.MovieId == movie.MovieId);
            if (movie == null)
            {
                throw new NullReferenceException("No movie found");
            }
            movieToUpdate.Title = movie.Title ?? movieToUpdate.Title;
            movieToUpdate.Description = movie.Description ?? movieToUpdate.Description;
            movieToUpdate.AmountOfScreenings = movie.AmountOfScreenings;
            movieToUpdate.MaxScreenings = movie.MaxScreenings;
            movieToUpdate.Language = movie.Language ?? movieToUpdate.Language;
            movieToUpdate.ReleaseYear = movie.ReleaseYear;
            movieToUpdate.DurationMinutes = movie.DurationMinutes;
            movieToUpdate.ImageUrl = movie.ImageUrl ?? movieToUpdate.ImageUrl;
            movieToUpdate.Price = movie.Price;
            movieToUpdate.TrailerId = movie.TrailerId;

            movieToUpdate.Actors = movie.Actors;

            movieToUpdate.Directors = movie.Directors;

            _trananDbContext.Movies.Update(movieToUpdate);

            await _trananDbContext.SaveChangesAsync();
            return movieToUpdate;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
    public async Task DeleteByIdAsync(int id)
    {
        var movieToDelete = await _trananDbContext.Movies.FindAsync(id);
        var deletedMovie = movieToDelete;
        if (movieToDelete != null)
        {
            _trananDbContext.Movies.Remove(movieToDelete);
            await _trananDbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync()
    {
        _trananDbContext.Movies.ToList().ForEach(m => _trananDbContext.Movies.Remove(m));
        await _trananDbContext.SaveChangesAsync();
    }
}
