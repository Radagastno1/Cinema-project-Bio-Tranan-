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
            return reservations;
        }
        catch (Exception)
        {
            return Enumerable.Empty<Reservation>();
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
            return reservationsForScreening;
        }
        catch (Exception)
        {
            return Enumerable.Empty<Reservation>();
        }
    }

    public async Task<Reservation> GetReservationByReservationCode(int reservationCode) //denna borde hämtas direkt från db, onödig process
    {
        try
        {
            var reservations = await _reservationRepository.GetAsync();
            var reservation = reservations.Where(r => r.ReservationCode == reservationCode).First();
            return reservation;
        }
        catch (Exception)
        {
            return new Reservation();
        }
    }

    public async Task<Reservation> CheckInReservationByCode(int code)
    {
        try
        {
            var checkedInReservation = await _reservationByScreeningRepository.CheckInReservation(
                code
            );
            return checkedInReservation;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
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
            throw new InvalidOperationException();
        }
    }

    public async Task<Reservation> Update(Reservation reservation)
    {
        try
        {
            var updatedReservation = await _reservationRepository.UpdateAsync(reservation);
            return updatedReservation;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task DeleteById(int id)
    {
        try
        {
            await _reservationRepository.DeleteByIdAsync(id);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}
