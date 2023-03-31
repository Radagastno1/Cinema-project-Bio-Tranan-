using Microsoft.EntityFrameworkCore;
using TrananAPI.DTO;
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
            var screening = _trananDbContext.MovieScreenings
                .Include(s => s.Movie)
                .ThenInclude(m => m.Actors)
                .Include(s => s.Movie)
                .ThenInclude(m => m.Directors)
                .Include(s => s.Theater);
            // .FirstOrDefault(s => s.MovieScreeningId == screeningId);

            if (screening != null)
            {
                // här kan du använda screening.Movie för att få tillhörande filmen
            }

            return await _trananDbContext.MovieScreenings
                    .Include(m => m.Movie)
                    .Include(m => m.Theater)
                    .ToListAsync() ?? new List<MovieScreening>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return null;
    }

    public async Task<MovieScreeningDTO> GetMovieScreeningById(int id)
    {
        try
        {
            var screening = await _trananDbContext.MovieScreenings.FindAsync(id);
            var movie = await _trananDbContext.Movies.FindAsync(screening.MovieId);
            var theater = await _trananDbContext.Theaters.FindAsync(screening.TheaterId);
            return Mapper.GenerateMovieScreeningDTO(screening, movie, theater);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<MovieScreeningDTO> CreateMovieScreening(MovieScreeningDTO movieScreeningDTO)
    {
        try
        {
            var movie = await _trananDbContext.Movies.FindAsync(movieScreeningDTO.MovieId);
            var theater = await _trananDbContext.Theaters.FindAsync(movieScreeningDTO.TheaterId);
            if (movie == null)
            {
                return null;
            }
            else
            {
                await _trananDbContext.MovieScreenings.AddAsync(
                    Mapper.GenerateMovieScreening(movieScreeningDTO, movie, theater)
                );
            }
            await _trananDbContext.SaveChangesAsync();
            return movieScreeningDTO;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task UpdateMovieScreening(MovieScreeningDTO movieScreeningDTO)
    {
        try
        {
            _trananDbContext.Update(movieScreeningDTO);
            await _trananDbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public async Task DeleteMovieScreening(MovieScreeningDTO movieScreeningDTO)
    {
        var movie = await _trananDbContext.Movies.FindAsync(movieScreeningDTO.MovieId);
        var theater = await _trananDbContext.Theaters.FindAsync(movieScreeningDTO.TheaterId);
        _trananDbContext.MovieScreenings.Remove(
            Mapper.GenerateMovieScreening(movieScreeningDTO, movie, theater)
        );
        await _trananDbContext.SaveChangesAsync();
    }

    public async Task DeleteMovieScreenings()
    {
        _trananDbContext.MovieScreenings
            .ToList()
            .ForEach(m => _trananDbContext.MovieScreenings.Remove(m));
        await _trananDbContext.SaveChangesAsync();
    }

    private MovieScreening GenerateRandomMovieScreening()
    {
        List<Seat> seats =
            new() { new Seat(4, 1, 2), new Seat(5, 2, 2), new Seat(6, 3, 2), new Seat(7, 4, 2) };
        List<Actor> actors = new() { new Actor("Beyonce", "Hawking") };

        Movie movie =
            new()
            {
                Title = "Bella Svan Prinsessan",
                Actors = actors,
                DurationSeconds = 123,
                Language = "svenska"
            };
        Theater theater = new() { Name = "Stora salen", Seats = seats };
        var movieScreening = new MovieScreening(1, DateTime.Now, movie, theater);

        return movieScreening;
    }
}
