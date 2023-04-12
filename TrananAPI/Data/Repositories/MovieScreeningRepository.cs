using Microsoft.EntityFrameworkCore;
using TrananAPI.DTO;
using TrananAPI.Models;

namespace TrananAPI.Data.Repository;

public class MovieScreeningRepository
{
    private readonly TrananDbContext _trananDbContext;

    public MovieScreeningRepository(TrananDbContext trananDbContext)
    {
        _trananDbContext = trananDbContext;
    }

    public async Task<List<MovieScreening>> GetUpcomingScreenings()
    {
        try
        {
            if (_trananDbContext.MovieScreenings == null)
            {
                return new List<MovieScreening>();
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

            foreach (var screening in screenings)
            {
                var allReservedSeats = await _trananDbContext.Reservations
                    .Where(r => r.MovieScreeningId == screening.MovieScreeningId)
                    .SelectMany(r => r.Seats)
                    .ToListAsync();

                screening.ReservedSeats = allReservedSeats ?? new List<Seat>();
            }

            return screenings;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return null;
    }

    public async Task<MovieScreening> GetMovieScreeningById(int id)
    {
        try
        {
            if (_trananDbContext.MovieScreenings == null)
            {
                return new MovieScreening();
            }
            var screening = await _trananDbContext.MovieScreenings
                .Include(s => s.Movie)
                .ThenInclude(m => m.Actors)
                .Include(s => s.Movie)
                .ThenInclude(m => m.Directors)
                .Include(s => s.Theater)
                .ThenInclude(t => t.Seats)
                .Where(s => s.DateAndTime > DateTime.Now)
                .Where(s => s.MovieScreeningId == id)
                .FirstOrDefaultAsync();

                var allReservedSeats = await _trananDbContext.Reservations
                    .Where(r => r.MovieScreeningId == screening.MovieScreeningId)
                    .SelectMany(r => r.Seats)
                    .ToListAsync();

                screening.ReservedSeats = allReservedSeats ?? new List<Seat>();

            return screening;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<MovieScreening> CreateMovieScreening(MovieScreening movieScreening)
    {
        using (var transaction = await _trananDbContext.Database.BeginTransactionAsync())
        {
            try
            {
                movieScreening.Movie = await _trananDbContext.Movies.FindAsync(
                    movieScreening.MovieId
                );
                movieScreening.Theater = await _trananDbContext.Theaters.FindAsync(
                    movieScreening.TheaterId
                );
                if (movieScreening.Movie == null || movieScreening.Theater == null)
                {
                    throw new NullReferenceException("Movie or theater can not be found.");
                }
                else if (
                    movieScreening.Movie.AmountOfScreenings >= movieScreening.Movie.MaxScreenings
                )
                {
                    throw new InvalidOperationException(
                        "Movie has maximum amount moviescreenings."
                    );
                }
                else if (await TheaterAvailable(movieScreening) == false)
                {
                    throw new InvalidOperationException(
                        "Theater not available at chosen time and day."
                    );
                }

                await _trananDbContext.MovieScreenings.AddAsync(movieScreening);
                await _trananDbContext.SaveChangesAsync();

                var recentlyAddedScreening = _trananDbContext.MovieScreenings
                    .OrderByDescending(s => s.MovieScreeningId)
                    .Include(s => s.Movie)
                    .Include(s => s.Movie.Actors)
                    .Include(s => s.Movie.Directors)
                    .Include(s => s.Theater)
                    .Include(s => s.Theater.Seats)
                    .FirstOrDefault();

                var movieToUpdate = recentlyAddedScreening.Movie;
                if (movieToUpdate.AmountOfScreenings == 0)
                {
                    movieToUpdate.AmountOfScreenings = 1;
                }
                else
                {
                    movieToUpdate.AmountOfScreenings++;
                }
                _trananDbContext.Movies.Update(movieToUpdate);
                await _trananDbContext.SaveChangesAsync();

                await transaction.CommitAsync();

                return recentlyAddedScreening;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw e;
            }
        }
    }

    public async Task<MovieScreening> UpdateMovieScreening(MovieScreening movieScreening)
    {
        try
        {
            var movieScreeningToUpdate = await _trananDbContext.MovieScreenings.FindAsync(
                movieScreening.MovieScreeningId
            );
            movieScreeningToUpdate.DateAndTime = movieScreening.DateAndTime;
            movieScreeningToUpdate.Movie = await _trananDbContext.Movies.FindAsync(
                movieScreening.MovieId
            );
            movieScreeningToUpdate.Theater = await _trananDbContext.Theaters.FindAsync(
                movieScreening.TheaterId
            );

            _trananDbContext.Update(movieScreeningToUpdate);
            await _trananDbContext.SaveChangesAsync();
            return movieScreeningToUpdate;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task DeleteMovieScreeningById(int id)
    {
        var movieScreeningToRemove = await _trananDbContext.MovieScreenings.FindAsync(id);
        _trananDbContext.MovieScreenings.Remove(movieScreeningToRemove);
        await _trananDbContext.SaveChangesAsync();
    }

    public async Task SaveChanges()
    {
        await _trananDbContext.SaveChangesAsync();
    }

    private async Task<bool> TheaterAvailable(MovieScreening movieScreening)
    {
        var currentScreeningStartTime = movieScreening.DateAndTime;
        var extraMinutes = 15;
        var currentScreeningEndTime = currentScreeningStartTime.AddMinutes(
            movieScreening.Movie.DurationMinutes + extraMinutes
        );

        var overlappingScreenings =
            await _trananDbContext.MovieScreenings
                .Where(m => m.TheaterId == movieScreening.TheaterId)
                .Where(
                    m =>
                        m.DateAndTime.AddMinutes(m.Movie.DurationMinutes + extraMinutes)
                            > currentScreeningStartTime
                        && m.DateAndTime < currentScreeningEndTime
                        && m.MovieScreeningId != movieScreening.MovieScreeningId
                )
                .ToListAsync() ?? new List<MovieScreening>();

        return overlappingScreenings.Count == 0;
    }
}
