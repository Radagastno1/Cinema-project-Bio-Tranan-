using Microsoft.EntityFrameworkCore;
using TrananAPI.DTO;
using TrananAPI.Models;

namespace TrananAPI.Data;

public class MovieRepository
{
    private readonly TrananDbContext _trananDbContext;

    public MovieRepository(TrananDbContext trananDbContext)
    {
        _trananDbContext = trananDbContext;
    }

    public async Task<List<MovieDTO>> GetMovies()
    {
        try
        {
            if (_trananDbContext.Movies.Count() < 1)
            {
                return new List<MovieDTO>();
            }
            return await _trananDbContext.Movies
                .Include(m => m.Actors)
                .Include(m => m.Directors)
                .Select(m => Mapper.GenerateMovieDTO(m))
                .ToListAsync();
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
            var movie = await _trananDbContext.Movies
                .Include(m => m.Actors)
                .Include(m => m.Directors)
                .FirstAsync(m => m.MovieId == id);

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
            List<Actor> actorsOfMovie = new();
            foreach (var actor in movieDTO.ActorDTOs)
            {
                var actorInDB = await _trananDbContext.Actors.FindAsync(actor.ActorId);
                if (actorInDB == null)
                {
                    var newActor = Mapper.GenerateActor(actor);
                    actorsOfMovie.Add(newActor);
                }
                else
                {
                    actorsOfMovie.Add(actorInDB);
                }
            }
            List<Director> directorsOfMovie = new();
            foreach (var director in movieDTO.DirectorDTOs)
            {
                var directorInDb = await _trananDbContext.Directors.FindAsync(director.DirectorId);
                if (directorInDb == null)
                {
                    var newDirector = Mapper.GenerateDirector(director);
                    directorsOfMovie.Add(newDirector);
                }
                else
                {
                    directorsOfMovie.Add(directorInDb);
                }
            }

            var newMovie = Mapper.GenerateMovie(movieDTO);
            newMovie.Actors = actorsOfMovie;
            newMovie.Directors = directorsOfMovie;
            await _trananDbContext.AddAsync(newMovie);
            await _trananDbContext.SaveChangesAsync();
            return movieDTO;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
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

    public async Task UpdateMovie(MovieDTO movieDTO)
    { //får vara ifyllda fält för uppdatering av filmen sen i mvc ?? 
        try
        {
            var movie = await _trananDbContext.Movies
                .Include(m => m.Actors)
                .Include(m => m.Directors)
                .FirstAsync(m => m.MovieId == movieDTO.MovieId);
            if (movie == null)
            {
                throw new Exception("No movie found");
            }
        

            movie.Title = movieDTO.Title ?? movie.Title;
            movie.Description = movieDTO.Description ?? movie.Description;
            movie.AmountOfScreenings = movieDTO.AmountOfScreenings;
            movie.MaxScreenings = movieDTO.MaxScreenings;
            movie.Language = movieDTO.Language ?? movieDTO.Language;
            movie.ReleaseYear = movieDTO.ReleaseYear;
            movie.DurationSeconds = movieDTO.DurationSeconds;

            movie.Actors = movieDTO.ActorDTOs.Select(a => Mapper.GenerateActor(a)).ToList();
  
            movie.Directors = movieDTO.DirectorDTOs.Select(d => Mapper.GenerateDirector(d)).ToList();

            _trananDbContext.Movies.Update(movie);
 
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
        var movieToDelete = await _trananDbContext.Movies.FindAsync(movieDTO.MovieId);
        _trananDbContext.Movies.Remove(movieToDelete);
        await _trananDbContext.SaveChangesAsync();
    }

    public async Task DeleteMovies()
    {
        _trananDbContext.Movies.ToList().ForEach(m => _trananDbContext.Movies.Remove(m));
        await _trananDbContext.SaveChangesAsync();
    }
}
