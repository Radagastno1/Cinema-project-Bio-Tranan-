// using TrananAPI.DTO;
// using TrananAPI.Models;

// namespace TrananAPI.Service;

// public class ReservationService
// {
//     private SeatService _seatService;

//     public ReservationService(SeatService seatService)
//     {
//         _seatService = seatService;
//     }

//     public async Task<Reservation> GenerateReservation(ReservationDTO reservationDTO)
//     {
//         var movieScreening = await _trananDbContext.MovieScreenings.FindAsync(
//             reservationDTO.MovieScreeningId
//         );
//         var customer = await _trananDbContext.Customers.FindAsync(
//             reservationDTO.CustomerDTO.CustomerId
//         );
//         var seats = await _seatService.GenerateSeatsFromIdsAsync(reservationDTO.SeatIds);

//         var reservation = Reservation.CreateReservation(
//             reservationDTO.Price,
//             movieScreening,
//             customer,
//             seats
//         );

//         return reservation;
//     }
// }
