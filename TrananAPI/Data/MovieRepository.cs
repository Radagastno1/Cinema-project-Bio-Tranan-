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
                await _trananDbContext.Movies.AddRangeAsync(GenerateRandomMovies());
                await _trananDbContext.SaveChangesAsync();
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

            var newMovie = Mapper.GenerateMovie(movieDTO);
            newMovie.Actors = actorsOfMovie;
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

    public async Task UpdateMovie(MovieDTO movieDTO)
    {
        try
        {
            var movie = await _trananDbContext.Movies
                .Include(m => m.Actors)
                .FirstAsync(m => m.MovieId == movieDTO.MovieId);
            if (movie == null)
            {
                throw new Exception("No movie found");
            }

            //får göra såhär annars skapar ny instans och då skapas ny film ist för upppdatera
            movie.Title = movieDTO.Title;
            movie.Language = movieDTO.Language;
            movie.ReleaseYear = movieDTO.ReleaseYear;
            movie.DurationSeconds = movieDTO.DurationSeconds;

            List<Actor> updatedActors = new();
            foreach (var actorDTO in movieDTO.ActorDTOs)
            {
                var existingActor = await _trananDbContext.Actors.FindAsync(actorDTO.ActorId);
                if (existingActor == null)
                {
                    _trananDbContext.Actors.Add(Mapper.GenerateActor(actorDTO));
                    var recentlyAddedActor = movie.Actors
                        .OrderByDescending(a => a.ActorId)
                        .FirstOrDefault();
                    updatedActors.Add(recentlyAddedActor);
                }
                else
                {
                    updatedActors.Add(existingActor);
                }
            }
            movie.Actors = updatedActors;
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
                new Movie("Harry Potter", 2023, "English", 208,"Let's go to hogwarts", actors),
                new Movie("Kalle Anka", 2023, "English", 208,"En anka som är tokig", actors),
                new Movie("Sagan om de sju", 2023, "English", 208,"Läskig men rolig", actors),
                new Movie("Milkshake", 2023, "English", 208,"My milkshake brings all..", actors),
                new Movie("Macarena", 2023, "English", 208,"Om macarena bandet med la ketchup", actors),
            };

        return movies;
    }
}
