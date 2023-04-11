using Microsoft.EntityFrameworkCore;
using TrananAPI.Models;

namespace TrananAPI.Data.Repository;

public class MovieRepository
{
    private readonly TrananDbContext _trananDbContext;

    public MovieRepository(TrananDbContext trananDbContext)
    {
        _trananDbContext = trananDbContext;
    }

    // private async Task ResetSeats()
    // {
    //     var seats = await _trananDbContext.Seats.ToListAsync();
    //     foreach (var seat in seats)
    //     {
    //         seat.IsBooked = false;
    //     }
    //     await _trananDbContext.SaveChangesAsync();
    // }

    public async Task<List<Movie>> GetMovies()
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
                .ToListAsync();
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<Movie> GetMovieById(int id)
    {
        try //include
        {
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

    public async Task<Movie> CreateMovie(Movie movie)
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

    public async Task<Actor> GetActorById(int actorId)
    {
        var actor = await _trananDbContext.Actors.FindAsync(actorId);
        if (actor == null)
        {
            return null;
        }
        return actor;
    }

    public async Task<Director> GetDirectorById(int directorId)
    {
        var director = await _trananDbContext.Directors.FindAsync(directorId);
        if (director == null)
        {
            return null;
        }
        return director;
    }

    // public async Task UpdateMovie(MovieDTO movieDTO)
    // {
    //     try
    //     {
    //         var movie = await _trananDbContext.Movies
    //             .Include(m => m.Actors)
    //             .Include(m => m.Directors)
    //             .FirstAsync(m => m.MovieId == movieDTO.MovieId);
    //         if (movie == null)
    //         {
    //             throw new Exception("No movie found");
    //         }

    //         //får göra såhär annars skapar ny instans och då skapas ny film ist för upppdatera
    //         movie.Title = movieDTO.Title;
    //         movie.Language = movieDTO.Language;
    //         movie.ReleaseYear = movieDTO.ReleaseYear;
    //         movie.DurationSeconds = movieDTO.DurationSeconds;

    //         List<Actor> updatedActors = new();
    //         foreach (var actorDTO in movieDTO.ActorDTOs)
    //         {
    //             var existingActor = await _trananDbContext.Actors.FindAsync(actorDTO.ActorId);
    //             if (existingActor == null)
    //             {
    //                 _trananDbContext.Actors.Add(Mapper.GenerateActor(actorDTO));
    //                 var recentlyAddedActor = movie.Actors
    //                     .OrderByDescending(a => a.ActorId)
    //                     .FirstOrDefault();
    //                 updatedActors.Add(recentlyAddedActor);
    //             }
    //             else
    //             {
    //                 updatedActors.Add(existingActor);
    //             }
    //         }
    //           List<Director> updatedDirectors = new();
    //         foreach (var directorDTO in movieDTO.DirectorDTOs)
    //         {
    //             var existingDirector = await _trananDbContext.Directors.FindAsync(directorDTO.DirectorId);
    //             if (existingDirector == null)
    //             {
    //                 _trananDbContext.Directors.Add(Mapper.GenerateDirector(directorDTO));
    //                 var recentlyAddedDirector = movie.Directors
    //                     .OrderByDescending(d => d.DirectorId)
    //                     .FirstOrDefault();
    //                 updatedDirectors.Add(recentlyAddedDirector);
    //             }
    //             else
    //             {
    //                 updatedDirectors.Add(existingDirector);
    //             }
    //         }
    //         movie.Actors = updatedActors;
    //         _trananDbContext.Movies.Update(movie);
    //         await _trananDbContext.SaveChangesAsync();
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e.Message);
    //     }
    // }

    public async Task<Movie> UpdateMovie(Movie movie)
    { //får vara ifyllda fält för uppdatering av filmen sen i mvc ??
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

    // public async Task<MovieDTO> AddMovie(MovieDTO movieDTO)
    // {
    //     await _trananDbContext.Movies.AddAsync(Mapper.GenerateMovie(movieDTO));
    //     await _trananDbContext.SaveChangesAsync();
    //     return movieDTO;
    // }

    public async Task DeleteMovieById(int id)
    {
        var movieToDelete = await _trananDbContext.Movies.FindAsync(id);
        var deletedMovie = movieToDelete;
        if (movieToDelete != null)
        {
            _trananDbContext.Movies.Remove(movieToDelete);
            await _trananDbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteMovies()
    {
        _trananDbContext.Movies.ToList().ForEach(m => _trananDbContext.Movies.Remove(m));
        await _trananDbContext.SaveChangesAsync();
    }
}
