using Core.Models;
using Core.Services;
using TrananAPI.DTO;
using TrananAPI.Service;

namespace TrananAPI.Services;

public class ReservationService
{
        private readonly ReservationCoreService _reservationCoreService;

    public ReservationService(ReservationCoreService reservationCoreService)
    {
        _reservationCoreService = reservationCoreService;
    }

    public async Task<IEnumerable<ReservationDTO>> GetReservations()
    {
        var reservations = await _reservationCoreService.GetReservations();
        return reservations.Select(r => Mapper.GenerateReservationDTO(r));
    }

    public async Task<IEnumerable<ReservationDTO>> GetReservationsByMovieScreening(
        int movieScreeningId
    )
    {
        var reservationsForScreening = await _reservationCoreService.GetReservationsByMovieScreening(
            movieScreeningId
        );
        return reservationsForScreening.Select(r => Mapper.GenerateReservationDTO(r));
    }

    public async Task<ReservationDTO> CreateReservation(ReservationDTO reservationDTO)
    {
        try
        {
            var newReservation = await Mapper.GenerateReservation(reservationDTO);
            var addedReservation = await _reservationCoreService.CreateReservation(newReservation);
            return Mapper.GenerateReservationDTO(addedReservation);
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

    public async Task<ReservationDTO> UpdateReservation(ReservationDTO reservationDTO)
    {
        var reservationToUpdate = await Mapper.GenerateReservation(reservationDTO);
        var updatedReservation = await _reservationCoreService.UpdateReservation(
            reservationToUpdate
        );
        return Mapper.GenerateReservationDTO(reservationToUpdate);
    }

    public async Task DeleteReservation(int reservationId)
    {
        await _reservationCoreService.DeleteReservation(reservationId);
    }
    // public async Task<Reservation> GenerateReservation(ReservationDTO reservationDTO)
    // {
    //     var movieScreening = await _trananDbContext.MovieScreenings.FindAsync(
    //         reservationDTO.MovieScreeningId
    //     );
    //     var customer = await _trananDbContext.Customers.FindAsync(
    //         reservationDTO.CustomerDTO.CustomerId
    //     );
    //     var seats = await _seatService.GenerateSeatsFromIdsAsync(reservationDTO.SeatIds);

    //     var reservation = Reservation.CreateReservation(
    //         reservationDTO.Price,
    //         movieScreening,
    //         customer,
    //         seats
    //     );

    //     return reservation;
    // }
}
