using Core.Models;
using Core.Interface;

namespace Core.Services;

public class ReservationService : IService<Reservation>, IReservationService
{
    private IRepository<Reservation> _reservationRepository;
    private IReservationRepository _reservationByScreeningRepository;

    public ReservationService(IRepository<Reservation> reservationRepository, IReservationRepository reservationByScreeningRepository)
    {
        _reservationRepository = reservationRepository;
        _reservationByScreeningRepository = reservationByScreeningRepository;
    }

    public async Task<IEnumerable<Reservation>> Get()
    {
        var reservations = await _reservationRepository.GetAsync();
        return reservations;
    }

    public async Task<IEnumerable<Reservation>> GetReservationsByMovieScreening(
        int movieScreeningId
    )
    {
        var reservationsForScreening =
            await _reservationByScreeningRepository.GetByScreeningIdAsync(movieScreeningId);
        return reservationsForScreening;
    }

    public async Task<Reservation> GetReservationByReservationCode(int reservationCode) //denna borde hämtas direkt från db, onödig process
    {
        var reservations = await _reservationRepository.GetAsync();
        var reservation = reservations.Where(r => r.ReservationCode == reservationCode).First();
        return reservation;
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
        var updatedReservation = await _reservationRepository.UpdateAsync(reservation);
        return updatedReservation;
    }

    public async Task DeleteById(int id)
    {
        await _reservationRepository.DeleteByIdAsync(id);
    }
}
