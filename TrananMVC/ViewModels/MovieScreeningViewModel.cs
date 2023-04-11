namespace TrananMVC.ViewModel;

public class MovieScreeningViewModel
{
     public int MovieScreeningId { get; set; }
    public DateTime DateAndTime { get; set; }
    // public MovieViewModel Movie { get; set; } //outgoiing
     public int MovieId { get; set; } //incommiing
    public string MovieTitle{get;set;}
    public string MovieImageUrl{get;set;}   
    // public int TheaterId { get; set; } //incoming
    public string TheaterName{get;set;}
    // public TheaterViewModel Theater { get; set; } //outgoing
    public List<SeatViewModel> AvailebleSeats{get;set;}

    public MovieScreeningViewModel() { }

    // public MovieScreeningViewModel(
    //     DateTime dateAndTime,
    //     int movieId,
    //     MovieViewModel movie,
    //     int theaterId,
    //     TheaterViewModel theater
    // )
    // {
    //     DateAndTime = dateAndTime;
    //     MovieId = movieId;
    //     Movie = movie;
    //     TheaterId = theaterId;
    //     Theater = theater;
    // }
}