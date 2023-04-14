using Core.Models;
using Core.Data.Repository;

namespace Core.Services;

public class ReservationCoreService
{
    private ReservationRepository _reservationRepository;

    public ReservationCoreService(ReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<IEnumerable<Reservation>> GetReservations()
    {
        var reservations = await _reservationRepository.GetReservations();
        return reservations;
    }

    public async Task<IEnumerable<Reservation>> GetReservationsByMovieScreening(
        int movieScreeningId
    )
    {
        var reservationsForScreening = await _reservationRepository.GetReservationsByScreeningId(
            movieScreeningId
        );
        return reservationsForScreening;
    }

    public async Task<Reservation> CreateReservation(Reservation reservation)
    {
        try
        {
            var addedReservation = await _reservationRepository.CreateReservation(reservation);
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
        var updatedReservation = await _reservationRepository.UpdateReservation(reservation);
        return updatedReservation;
    }

    public async Task DeleteReservation(int reservationId)
    {
        await _reservationRepository.DeleteReservation(reservationId);
    }
}
