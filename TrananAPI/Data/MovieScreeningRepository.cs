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

    public async Task<List<MovieScreeningOutgoingDTO>> GetUpcomingScreenings()
    {
        try
        {
            if (_trananDbContext.MovieScreenings == null)
            {
                // await _trananDbContext.MovieScreenings.AddAsync(GenerateRandomMovieScreening());
                // await _trananDbContext.SaveChangesAsync();
                return null;
            }
            var screenings = await _trananDbContext.MovieScreenings
                .Include(s => s.Movie)
                .ThenInclude(m => m.Actors)
                .Include(s => s.Movie)
                .ThenInclude(m => m.Directors)
                .Include(s => s.Theater)
                .ThenInclude(t => t.Seats)
                .Where(s => s.DateAndTime > DateTime.Now)
                .ToListAsync();
            return screenings.Select(s => Mapper.GenerateMovieScreeningOutcomingDTO(s)).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return null;
    }

    public async Task<MovieScreeningOutgoingDTO> GetMovieScreeningById(int id)
    {
        try
        {
            var screening = await _trananDbContext.MovieScreenings.FindAsync(id);
            var movie = await _trananDbContext.Movies.FindAsync(screening.MovieId);
            var theater = await _trananDbContext.Theaters.FindAsync(screening.TheaterId);
            return Mapper.GenerateMovieScreeningOutcomingDTO(screening);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<MovieScreeningOutgoingDTO> CreateMovieScreening(
        MovieScreeningIncomingDTO movieScreeningDTO
    )
    {
        try
        {
            var movie = await _trananDbContext.Movies.FirstAsync(
                m => m.MovieId == movieScreeningDTO.MovieId
            );
            var theater = await _trananDbContext.Theaters.FirstAsync(
                m => m.TheaterId == movieScreeningDTO.TheaterId
            );
            if (movie == null || theater == null)
            {
                throw new Exception("movie or theater can not be found.");
            }

            await _trananDbContext.MovieScreenings.AddAsync(
                Mapper.GenerateMovieScreeningFromIncomingDTO(movieScreeningDTO, movie, theater)
            );
            await _trananDbContext.SaveChangesAsync();

            var recentlyAddedScreening = _trananDbContext.MovieScreenings
                .OrderByDescending(s => s.MovieScreeningId)
                .Include(s => s.Movie)
                .Include(s => s.Movie.Actors)
                .Include(s => s.Movie.Directors)
                .Include(s => s.Theater)
                .Include(s => s.Theater.Seats)
                .FirstOrDefault();
            return Mapper.GenerateMovieScreeningOutcomingDTO(recentlyAddedScreening);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task UpdateMovieScreening(MovieScreeningIncomingDTO movieScreeningDTO)
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

    public async Task DeleteMovieScreening(MovieScreeningIncomingDTO movieScreeningDTO)
    {
        var movie = await _trananDbContext.Movies.FindAsync(movieScreeningDTO.MovieId);
        var theater = await _trananDbContext.Theaters.FindAsync(movieScreeningDTO.TheaterId);
        _trananDbContext.MovieScreenings.Remove(
            Mapper.GenerateMovieScreeningFromIncomingDTO(movieScreeningDTO, movie, theater)
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

    // private MovieScreening GenerateRandomMovieScreening()
    // {
    //     List<Seat> seats =
    //         new() { new Seat(1, 2), new Seat(2, 2), new Seat(3, 2), new Seat(4, 2) };
    //     List<Actor> actors = new() { new Actor("Beyonce", "Hawking") };

    //     Movie movie =
    //         new()
    //         {
    //             Title = "Bella Svan Prinsessan",
    //             Actors = actors,
    //             DurationSeconds = 123,
    //             Language = "svenska"
    //         };
    //     Theater theater = new() { Name = "Stora salen", Seats = seats };
    //     var movieScreening = new MovieScreening(DateTime.Now.AddDays(4), movie, theater);

    //     return movieScreening;
    // }
}
