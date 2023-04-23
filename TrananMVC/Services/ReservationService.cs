using TrananMVC.ViewModel;
using TrananMVC.Interface;
using Core.Models;

namespace TrananMVC.Service;

public class ReservationService : IReservationService
{
    private readonly Core.Interface.IService<Reservation> _coreReservationService;
    private readonly Core.Interface.IService<MovieScreening> _coreScreeningService;
    private readonly Core.Interface.IService<Movie> _coreMovieService;

    public ReservationService(
      Core.Interface.IService<Reservation> coreReservationService, Core.Interface.IService<MovieScreening> coreScreeningService, 
      Core.Interface.IService<Movie> coreMovieService
    )
    {
        _coreReservationService = coreReservationService;
        _coreMovieService = coreMovieService;
        _coreScreeningService = coreScreeningService;
    }
    //   public static async Task<Reservation> CreateReservationAsync(
    //     int reservationId,
    //     decimal price,
    //     int movieScreeningId,
    //     Customer customer,
    //     List<Seat> seats,
    //     bool isCheckedIn = false
    // )
    // {
    //     var reservation = new Reservation()
    //     {
    //         Price = price,
    //         MovieScreeningId = movieScreeningId,
    //         Customer = customer,
    //         Seats = seats,
    //         IsCheckedIn = isCheckedIn
    //     };

    //     reservation.ReservationCode = await GenerateReservationCodeAsync();
    //     return reservation;
    // }

    // private static async Task<int> GenerateReservationCodeAsync()
    // {
    //     int randomNumber = await GetRandomNumberFromAPI();

    //     if (randomNumber == 0)
    //     {
    //         throw new Exception("Reservation code is unavailable. Please contact admin.");
    //     }
    //     return randomNumber;
    // }

    // private static async Task<int> GetRandomNumberFromAPI()
    // {
    //     string url = "http://www.randomnumberapi.com/api/v1.0/random?min=100&max=1000";
    //     HttpClient httpClient = new();
    //     try
    //     {
    //         var randomNumberArray = await httpClient.GetFromJsonAsync<int[]>(url);
    //         int randomNumber = randomNumberArray[0];
    //         return randomNumber;
    //     }
    //     catch (HttpRequestException)
    //     {
    //         return 0;
    //     }
    // }

    public async Task<CreatedReservationViewModel> CreateReservation(
        ReservationViewModel reservationViewModel
    )
    {
        try
        {
            var reservation = Mapper.GenerateReservation(reservationViewModel);
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
