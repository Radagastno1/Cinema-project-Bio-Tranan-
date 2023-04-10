using Microsoft.EntityFrameworkCore;
using TrananAPI.Models;

namespace TrananAPI.Data.Repository;

public class ReservationRepository
{
    private readonly TrananDbContext _trananDbContext;

    public ReservationRepository(TrananDbContext trananDbContext)
    {
        _trananDbContext = trananDbContext;
    }

    public async Task<List<Reservation>> GetReservations()
    {
        try
        {
            if (_trananDbContext.Movies.Count() < 1)
            {
                return new List<Reservation>();
            }
            return await _trananDbContext.Reservations
                .Include(r => r.Seats)
                .Include(r => r.Customer)
                .Include(r => r.MovieScreening)
                .ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<List<Reservation>> GetReservationsByScreeningId(int screeningId)
    {
        try
        {
            var reservations = await _trananDbContext.Reservations
                .Include(r => r.Customer)
                .Include(r => r.MovieScreening)
                .Include(r => r.Seats)
                .Where(r => r.MovieScreeningId == screeningId)
                .ToListAsync();

            return reservations;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<Reservation> CreateReservation(Reservation reservation)
    {
        List<Seat> seats = new List<Seat>();

        var foundMovieScreening = await _trananDbContext.MovieScreenings
            .Include(m => m.Theater)
            .ThenInclude(t => t.Seats)
            .Where(m => m.MovieScreeningId == reservation.MovieScreeningId)
            .FirstOrDefaultAsync();

        if (foundMovieScreening == null)
        {
            throw new InvalidOperationException("Movie screening not found.");
        }

        var allReservedSeats = await _trananDbContext.Reservations
            .Where(r => r.MovieScreeningId == foundMovieScreening.MovieScreeningId)
            .SelectMany(r => r.Seats)
            .ToListAsync();

        foreach (var seat in reservation.Seats)
        {
            var foundSeat = foundMovieScreening.Theater.Seats.FirstOrDefault(
                s => s.SeatId == seat.SeatId
            );

            if (foundSeat == null)
            {
                throw new InvalidOperationException("Seat not found.");
            }

            if (!allReservedSeats.Any(s => s.SeatId == foundSeat.SeatId))
            {
                foundSeat.IsBooked = true;
                foundSeat.MovieScreenings.Add(foundMovieScreening);

                seats.Add(foundSeat);
            }
            else
            {
                throw new InvalidOperationException("Seat is not available.");
            }
        }

        reservation.Seats = seats;
        await _trananDbContext.Reservations.AddAsync(reservation);
        await _trananDbContext.SaveChangesAsync();

        return reservation;
    }

    public async Task<Reservation> UpdateReservation(Reservation reservation)
    {
        var reservationToUpdate = await _trananDbContext.Reservations.FindAsync(
            reservation.ReservationId
        );
        reservationToUpdate.Customer = reservation.Customer ?? reservationToUpdate.Customer;
        reservationToUpdate.MovieScreening =
            reservation.MovieScreening ?? reservationToUpdate.MovieScreening;
        reservationToUpdate.Price = reservation.Price;
        reservationToUpdate.Seats = reservation.Seats;
        reservationToUpdate.ReservationCode = reservation.ReservationCode;

        _trananDbContext.Reservations.Update(reservationToUpdate);
        await _trananDbContext.SaveChangesAsync();
        return reservationToUpdate;
    }

    public async Task DeleteReservation(int reservationId)
    {
        var reservationToDelete = await _trananDbContext.Reservations.FindAsync(reservationId);
        _trananDbContext.Reservations.Remove(reservationToDelete);
    }
    // public async Task UpdateMovie(MovieDTO movieDTO)
    // {
    //     try
    //     {
    //         var movie = await _trananDbContext.Movies
    //             .Include(m => m.Actors)
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
    //         movie.Actors = updatedActors;
    //         _trananDbContext.Movies.Update(movie);
    //         await _trananDbContext.SaveChangesAsync();
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e.Message);
    //     }
    // }

    // public async Task<MovieDTO> AddMovie(MovieDTO movieDTO)
    // {
    //     await _trananDbContext.Movies.AddAsync(Mapper.GenerateMovie(movieDTO));
    //     await _trananDbContext.SaveChangesAsync();
    //     return movieDTO;
    // }

    // public async Task DeleteMovie(MovieDTO movieDTO)
    // {
    //     var movieToDelete = await _trananDbContext.Movies.FindAsync(movieDTO.MovieId);
    //     _trananDbContext.Movies.Remove(movieToDelete);
    //     await _trananDbContext.SaveChangesAsync();
    // }

    // public async Task DeleteMovies()
    // {
    //     _trananDbContext.Movies.ToList().ForEach(m => _trananDbContext.Movies.Remove(m));
    //     await _trananDbContext.SaveChangesAsync();
    // }

    // private List<Movie> GenerateRandomMovies()
    // {
    //     var actors = new List<Actor>()
    //     {
    //         new Actor("Isabella", "Tortellini"),
    //         new Actor("Henrik", "Byström")
    //     };
    //     List<Movie> movies =
    //         new()
    //         {
    //             new Movie("Harry Potter", 2023, "English", 208,"Let's go to hogwarts", actors),
    //             new Movie("Kalle Anka", 2023, "English", 208,"En anka som är tokig", actors),
    //             new Movie("Sagan om de sju", 2023, "English", 208,"Läskig men rolig", actors),
    //             new Movie("Milkshake", 2023, "English", 208,"My milkshake brings all..", actors),
    //             new Movie("Macarena", 2023, "English", 208,"Om macarena bandet med la ketchup", actors),
    //         };

    //     return movies;
    // }
}
