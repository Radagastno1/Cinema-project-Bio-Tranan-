using Microsoft.EntityFrameworkCore;
using Core.Models;
using Core.Interface;

namespace Core.Data.Repository;

public class ReservationRepository : IRepository<Reservation>, IReservationRepository
{
    private readonly TrananDbContext _trananDbContext;

    public ReservationRepository(TrananDbContext trananDbContext)
    {
        _trananDbContext = trananDbContext;
    }

    public async Task<List<Reservation>> GetAsync()
    {
        try
        {
            if (_trananDbContext.Reservations.Count() < 1)
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

    public async Task<List<Reservation>> GetByScreeningIdAsync(int screeningId)
    {
        try
        {
            var reservations = await _trananDbContext.Reservations
                .Include(r => r.Seats)
                .Include(r => r.Customer)
                .Include(r => r.MovieScreening)
                .Where(r => r.MovieScreeningId == screeningId)
                .ToListAsync();

            if (reservations != null && reservations.Any())
            {
                return reservations;
            }

            return new List<Reservation>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<Reservation> GetByIdAsync(int reservationId)
    {
        throw new NotImplementedException();
    }

    public async Task<Reservation> CreateAsync(Reservation reservation)
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

    public async Task<Reservation> UpdateAsync(Reservation reservation)
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

    public async Task DeleteByIdAsync(int reservationId)
    {
        var reservationToDelete = await _trananDbContext.Reservations.FindAsync(reservationId);
        _trananDbContext.Reservations.Remove(reservationToDelete);

        var SeatsToReset = await _trananDbContext.Seats
            .Where(s => s.Reservations.Any(r => r.ReservationId == reservationId))
            .ToListAsync();

        SeatsToReset.ForEach(s => s.IsBooked = false);

        await _trananDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync()
    {
        throw new NotImplementedException();
    }
}
