using TrananMVC.ViewModel;
using Core.Interface;
using Core.Models;

namespace TrananMVC.Service;

public class ReservationService
{
    private readonly IService<Reservation> _coreReservationService;
    private readonly IService<MovieScreening> _coreScreeningService;
    private readonly IService<Movie> _coreMovieService;

    public ReservationService(
      IService<Reservation> coreReservationService, IService<MovieScreening> coreScreeningService, 
      IService<Movie> coreMovieService
    )
    {
        _coreReservationService = coreReservationService;
        _coreMovieService = coreMovieService;
        _coreScreeningService = coreScreeningService;
    }

    public async Task<CreatedReservationViewModel> CreateReservation(
        ReservationViewModel reservationViewModel
    )
    {
        try
        {
            var reservation = await Mapper.GenerateReservation(reservationViewModel);
            var addedReservation = await _coreReservationService.Create(reservation);
            var movieScreening = await _coreScreeningService.GetById(
                addedReservation.MovieScreeningId
            );
            var movie = await _coreMovieService.GetById(movieScreening.MovieId);
            if (addedReservation == null || movieScreening == null)
            {
                throw new NullReferenceException("Något gick fel med att hämta reservationen.");
            }

            return Mapper.GenerateCreatedReservationViewModel(
                movieScreening,
                addedReservation,
                movie
            );
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<bool> DeleteReservationById(int reservationId)
    {
        try
        {
            await _coreReservationService.DeleteById(reservationId);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
