namespace TrananMVC.ViewModel;

public class CreatedReservationViewModel
{
    // public ReservationViewModel ReservationViewModel{get;set;} 
    public int ReservationId { get; set; }
    public int ReservationCode { get; set; }
    public decimal Price { get; set; }
    public int MovieScreeningId { get; set; }
    public string FirstName{get;set;}
    public string LastName{get;set;}
    public string PhoneNumber{get;set;}
    public string Email{get;set;}
    public List<int> SeatIds { get; set; }
    public DateTime DateAndTime { get; set; }
    // public int MovieId { get; set; } 
    public string MovieTitle { get; set; }
    public string MovieImageUrl { get; set; }
    public string TheaterName { get; set; }

    public CreatedReservationViewModel(int reservationId, int reservationCode, decimal price, string firstName, string lastName, string phoneNumber, string email, List<int>seatIds, DateTime dateAndTime, string movieTitle, string movieImageUrl, string theaterName)
    {
        ReservationId = reservationId;
        ReservationCode = reservationCode;
        Price = price;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Email = email;
        SeatIds = seatIds;
        DateAndTime = dateAndTime;
        MovieTitle = movieTitle;
        MovieImageUrl = movieImageUrl;
        TheaterName = theaterName;
    }
    public CreatedReservationViewModel(){}
}
