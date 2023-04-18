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

    public async Task<IEnumerable<Reservation>> GetAsync()
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
            return null;
            //logga
        }
    }

    public async Task<IEnumerable<Reservation>> GetByScreeningIdAsync(int screeningId)
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
            return null;
            //logga
        }
    }

    public async Task<Reservation> GetByIdAsync(int reservationId)
    {
        throw new NotImplementedException();
    }

    public async Task<Reservation> CreateAsync(Reservation reservation)
    {
        try
        {
            List<Seat> seats = new List<Seat>();

            var foundMovieScreening = await _trananDbContext.MovieScreenings
                .Include(m => m.Theater)
                .ThenInclude(t => t.Seats)
                .Where(m => m.MovieScreeningId == reservation.MovieScreeningId)
                .FirstOrDefaultAsync();

            if (foundMovieScreening == null)
            {
                return null;
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
                    return null;
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
        catch (Exception e)
        {
            return null;
            //logga
        }
    }

    public async Task<Reservation> UpdateAsync(Reservation reservation)
    {
        try
        {
            var reservationToUpdate = await _trananDbContext.Reservations.FindAsync(
                reservation.ReservationId
            );
            if (reservationToUpdate == null)
            {
                return null;
            }
            reservationToUpdate.Customer = reservation.Customer ?? reservationToUpdate.Customer;
            reservationToUpdate.MovieScreening =
                reservation.MovieScreening ?? reservationToUpdate.MovieScreening;
            reservationToUpdate.Price = reservation.Price;
            reservationToUpdate.Seats = reservation.Seats;
            reservationToUpdate.ReservationCode = reservation.ReservationCode;
            reservationToUpdate.IsCheckedIn = reservation.IsCheckedIn;

            _trananDbContext.Reservations.Update(reservationToUpdate);
            await _trananDbContext.SaveChangesAsync();
            return reservationToUpdate;
        }
        catch (ArgumentNullException e)
        {
            return null;
        }
    }

    public async Task DeleteByIdAsync(int reservationId)
    {
        try
        {
            var reservationToDelete = await _trananDbContext.Reservations.FindAsync(reservationId);
            _trananDbContext.Reservations.Remove(reservationToDelete);

            var SeatsToReset = await _trananDbContext.Seats
                .Where(s => s.Reservations.Any(r => r.ReservationId == reservationId))
                .ToListAsync();

            SeatsToReset.ForEach(s => s.IsBooked = false);

            await _trananDbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task DeleteAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Reservation> CheckInReservation(int reservationCode)
    {
        try
        {
            var reservationToCheckIn = await _trananDbContext.Reservations
                .Where(r => r.ReservationCode == reservationCode)
                .FirstOrDefaultAsync();
            if (reservationToCheckIn == null)
            {
                return null;
            }
            reservationToCheckIn.IsCheckedIn = true;
            _trananDbContext.Reservations.Update(reservationToCheckIn);
            await _trananDbContext.SaveChangesAsync();
            var updatedReservation = await _trananDbContext.Reservations
                .Include(r => r.Seats)
                .Include(r => r.Customer)
                .Include(r => r.MovieScreening)
                .Where(r => r.ReservationId == reservationToCheckIn.ReservationId)
                .FirstOrDefaultAsync();
            return updatedReservation;
        }
        catch (Exception e)
        {
            return null;
        }
    }
}
