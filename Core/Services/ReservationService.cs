using Core.Models;
using Core.Interface;

namespace Core.Services;

public class ReservationService : IService<Reservation>, IReservationService
{
    private IRepository<Reservation> _reservationRepository;
    private IReservationRepository _reservationByScreeningRepository;

    public ReservationService(
        IRepository<Reservation> reservationRepository,
        IReservationRepository reservationByScreeningRepository
    )
    {
        _reservationRepository = reservationRepository;
        _reservationByScreeningRepository = reservationByScreeningRepository;
    }

    public async Task<IEnumerable<Reservation>> Get()
    {
        try
        {
            var reservations = await _reservationRepository.GetAsync();
            if(reservations == null)
            {
                return Enumerable.Empty<Reservation>();
            }
            return reservations;
        }
        catch (Exception)
        {
           throw new Exception("Failed to get reservations from database.");
        }
    }

    public async Task<IEnumerable<Reservation>> GetReservationsByMovieScreening(
        int movieScreeningId
    )
    {
        try
        {
            var reservationsForScreening =
                await _reservationByScreeningRepository.GetByScreeningIdAsync(movieScreeningId);
                if(reservationsForScreening == null)
                {
                    return Enumerable.Empty<Reservation>();
                }
            return reservationsForScreening;
        }
        catch (Exception)
        {
            throw new Exception("Failed to get reservations by movie screening.");
        }
    }

    public async Task<Reservation> GetReservationByReservationCode(int reservationCode) //denna borde hämtas direkt från db, onödig process
    {
        try
        {
            var reservations = await _reservationRepository.GetAsync();
            if(reservations == null)
            {
                return null;
            }
            var reservation = reservations.Where(r => r.ReservationCode == reservationCode).First();
            if(reservation == null)
            {
                return null;
            }
            return reservation;
        }
        catch (Exception)
        {
            throw new Exception("Failed to get reservation by reservationcode.");
        }
    }

    public async Task<Reservation> CheckInReservationByCode(int code)
    {
        try
        {
            var checkedInReservation = await _reservationByScreeningRepository.CheckInReservation(
                code
            );
            if(checkedInReservation == null)
            {
                return null;
            }
            return checkedInReservation;
        }
        catch (Exception)
        {
            throw new Exception("Failed to check in reservation in database.");
        }
    }

    public async Task<Reservation> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Reservation> Create(Reservation reservation)
    {
        try
        {
            var addedReservation = await _reservationRepository.CreateAsync(reservation);
            return addedReservation;
        }
        catch (InvalidOperationException e)
        {
            if (e.Message == "Seat is not available.")
            {
                throw new InvalidOperationException(e.Message);
            }
            if (e.Message == "Seat not found.")
            {
                throw new InvalidOperationException(e.Message);
            }
            throw new InvalidOperationException("Failed to create reservation.");
        }
    }

    public async Task<Reservation> Update(Reservation reservation)
    {
        try
        {
            var updatedReservation = await _reservationRepository.UpdateAsync(reservation);
            if(updatedReservation == null)
            {
                return null;
            }
            return updatedReservation;
        }
        catch (Exception)
        {
            throw new Exception("Failed to update reservation.");
        }
    }

    public async Task DeleteById(int id)
    {
        try
        {
            await _reservationRepository.DeleteByIdAsync(id);
        }
        catch (Exception)
        {
            throw new Exception("Failed to delete reservation by id.");
        }
    }
}
