using Core.Models;
using Core.Interface;

namespace Core.Services;

public class ReservationCoreService
{
    private IRepository<Reservation> _reservationRepository;
    private IReservationRepository _reservationByScreeningRepository;

    public ReservationCoreService(IRepository<Reservation> reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<IEnumerable<Reservation>> GetReservations()
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

    public async Task<Reservation> CreateReservation(Reservation reservation)
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

    public async Task<Reservation> UpdateReservation(Reservation reservation)
    {
        var updatedReservation = await _reservationRepository.UpdateAsync(reservation);
        return updatedReservation;
    }

    public async Task DeleteReservation(int reservationId)
    {
        await _reservationRepository.DeleteByIdAsync(reservationId);
    }
}
