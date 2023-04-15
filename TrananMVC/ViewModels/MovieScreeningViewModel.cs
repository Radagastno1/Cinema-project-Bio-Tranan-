using System.Linq;

namespace TrananMVC.ViewModel;

public class MovieScreeningViewModel
{
    public int MovieScreeningId { get; set; }
    public DateTime DateAndTime { get; set; }
    public int MovieId { get; set; }
    public string MovieTitle { get; set; }
    public string MovieImageUrl { get; set; }
    public string TheaterName { get; set; }
    public decimal PricePerPerson { get; set; }
    public List<SeatViewModel> AllSeats { get; set; } = new();
    public List<SeatViewModel> AvailableSeats { get; set; } = new();
    public MovieScreeningViewModel(){}
    public MovieScreeningViewModel(int id, DateTime dateAndTime, int movieId, string movieTitle,
    string movieImageUrl, string theaterName, decimal pricePerPerson, List<SeatViewModel> allSeats)
    {
        MovieScreeningId = id;
        DateAndTime = dateAndTime;
        MovieId = movieId;
        MovieTitle = movieTitle;
        MovieImageUrl = movieImageUrl;
        TheaterName = theaterName;
        PricePerPerson = pricePerPerson;
        AllSeats = allSeats;
        AvailableSeats = AllSeats.Where(s => !s.IsBooked && !s.IsNotBookable).ToList() ?? new List<SeatViewModel>();
    }
}
