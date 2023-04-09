using TrananAPI.DTO;
using TrananAPI.Models;
using TrananAPI.Data.Repository;

namespace TrananAPI.Service;

public class ReservationService
{
    private ReservationRepository _reservationRepository;

    public ReservationService(ReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<IEnumerable<ReservationDTO>> GetReservations()
    {
        var reservations = await _reservationRepository.GetReservations();
        return reservations.Select(r => Mapper.GenerateReservationDTO(r));
    }

    public async Task<IEnumerable<ReservationDTO>> GetReservationsByMovieScreening(
        int movieScreeningId
    )
    {
        var reservationsForScreening = await _reservationRepository.GetReservationsByScreeningId(
            movieScreeningId
        );
        return reservationsForScreening.Select(r => Mapper.GenerateReservationDTO(r));
    }

    public async Task<ReservationDTO> CreateReservation(ReservationDTO reservationDTO)
    {
        var newReservation = await Mapper.GenerateReservation(reservationDTO);
        var addedReservation = await _reservationRepository.CreateReservation(newReservation);
        return Mapper.GenerateReservationDTO(addedReservation);
    }

    public async Task<ReservationDTO> UpdateReservation(ReservationDTO reservationDTO)
    {
        var reservationToUpdate = await Mapper.GenerateReservation(reservationDTO);
        var updatedReservation = await _reservationRepository.UpdateReservation(
            reservationToUpdate
        );
        return Mapper.GenerateReservationDTO(reservationToUpdate);
    }

    public async Task DeleteReservation(int reservationId) 
    {
        await _reservationRepository.DeleteReservation(reservationId);
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
