using TrananMVC.ViewModel;
using Core.Services;

namespace TrananMVC.Service;

public class ReservationService
{
    private readonly ReservationCoreService _reservationCoreService;
    private readonly MovieScreeningCoreService _movieScreeningCoreService;
    private readonly MovieCoreService _movieCoreService;
    public ReservationService(
       ReservationCoreService reservationCoreService, MovieScreeningCoreService movieScreeningCoreService,
       MovieCoreService movieCoreService
    )
    {
        _reservationCoreService = reservationCoreService;
        _movieScreeningCoreService = movieScreeningCoreService;
        _movieCoreService = movieCoreService;
    }

    public async Task<CreatedReservationViewModel> CreateReservation(
        ReservationViewModel reservationViewModel
    )
    {
        try
        {
            var reservation = await Mapper.GenerateReservation(reservationViewModel);
            var addedReservation = await _reservationCoreService.CreateReservation(reservation);
            var movieScreening = await _movieScreeningCoreService.GetMovieScreeningById(
                addedReservation.MovieScreeningId
            );
            var movie = await _movieCoreService.GetMovieById(movieScreening.MovieId);
            if(addedReservation == null || movieScreening == null)
            {
                throw new NullReferenceException("Något gick fel med att hämta reservationen.");
            }
            return Mapper.GenerateCreatedReservationViewModel(movieScreening, addedReservation, movie);
        }
        catch (Exception e)
        {
            return null;
        }
    }
}
