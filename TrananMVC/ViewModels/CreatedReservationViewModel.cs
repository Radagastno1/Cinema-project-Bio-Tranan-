using TrananMVC.Interface;

namespace TrananMVC.ViewModel;

public class CreatedReservationViewModel : IViewModel
{
    // public ReservationViewModel ReservationViewModel{get;set;} 
    public int Id { get; set; }
    public int ReservationCode { get; set; }
    public decimal Price { get; set; }
    public int MovieScreeningId { get; set; }
    public string FirstName{get;set;}
    public string LastName{get;set;}
    public string PhoneNumber{get;set;}
    public string Email{get;set;}
    public DateTime DateAndTime { get; set; }
    public List<SeatViewModel> ReservedSeats{get;set;} 
    public string MovieTitle { get; set; }
    public string MovieImageUrl { get; set; }
    public string TheaterName { get; set; }

    public CreatedReservationViewModel(int reservationId, int reservationCode, decimal price, string firstName, string lastName, string phoneNumber, string email, DateTime dateAndTime, string movieTitle, string movieImageUrl, string theaterName, List<SeatViewModel> reservedSeats)
    {
        Id = reservationId;
        ReservationCode = reservationCode;
        Price = price;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Email = email;
        DateAndTime = dateAndTime;
        MovieTitle = movieTitle;
        MovieImageUrl = movieImageUrl;
        TheaterName = theaterName;
        ReservedSeats = reservedSeats;
    }
    public CreatedReservationViewModel(){}
}
