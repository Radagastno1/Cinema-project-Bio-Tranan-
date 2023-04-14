using TrananMVC.ViewModel;
using Core.Services;

namespace TrananMVC.Service;

public class ReservationService
{
    private readonly ReservationCoreService _reservationCoreService;
    private readonly MovieScreeningCoreService _movieScreeningCoreService;
    public ReservationService(
       ReservationCoreService reservationCoreService, MovieScreeningCoreService movieScreeningCoreService
    )
    {
        _reservationCoreService = reservationCoreService;
        _movieScreeningCoreService = movieScreeningCoreService;
    }

    public async Task<CreatedReservationViewModel> CreateReservation(
        ReservationViewModel reservationViewModel
    )
    {
        try
        {
            var reservation = Mapper.GenerateReservation(reservationViewModel);
            var addedReservation = await _reservationCoreService.CreateReservation(reservation);
            var movieScreening = await _movieScreeningCoreService.GetMovieScreeningById(
                addedReservation.MovieScreeningId
            );
            return Mapper.GenerateCreatedReservationViewModel(movieScreening, addedReservation);
        }
        catch (Exception e)
        {
            return null;
        }
    }
}
