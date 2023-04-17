using System.Net.Http.Json;

namespace Core.Models;

public class Reservation
{
    public int ReservationId { get; set; }
    public decimal Price { get; set; }
    public int ReservationCode { get; set; }
    public int MovieScreeningId { get; set; }
    public MovieScreening MovieScreening { get; set; } 
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public List<Seat> Seats { get; set; }
    public bool IsCheckedIn{get;set;} 

    public Reservation() { }
    public static async Task<Reservation> CreateReservationAsync(
        int reservationId,
        decimal price,
        int movieScreeningId,
        Customer customer,
        List<Seat> seats, bool isCheckedIn
    )
    {
        var reservation = new Reservation()
        {
            Price = price,
            MovieScreeningId = movieScreeningId,
            Customer = customer,
            Seats = seats,
            IsCheckedIn = isCheckedIn
        };

        reservation.ReservationCode = await reservation.GenerateReservationCodeAsync();
        return reservation;
    }

    private async Task<int> GenerateReservationCodeAsync()
    {
        int randomNumber = await GetRandomNumberFromAPI();

        if (randomNumber == 0)
        {
            throw new Exception("Reservation code is unavailable. Please contact admin.");
        }
        return randomNumber;
    }

    //denna i service klassen bättrE?
    private async Task<int> GetRandomNumberFromAPI()
    {
        string url = "http://www.randomnumberapi.com/api/v1.0/random?min=100&max=1000";
        HttpClient httpClient = new();
        try
        {
            var randomNumberArray = await httpClient.GetFromJsonAsync<int[]>(url);
            int randomNumber = randomNumberArray[0];
            return randomNumber;
        }
        catch (HttpRequestException)
        {
            return 0;
        }
    }
}
