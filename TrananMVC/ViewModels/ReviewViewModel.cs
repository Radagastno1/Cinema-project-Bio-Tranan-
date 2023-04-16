using Core.Models;

namespace TrananMVC.ViewModel;

public class ReviewViewModel
{
    public string Alias{get;set;} //sätt i models i ccore med  och uppdatera db
    public int Rating { get; set; }
    public string Comment { get; set; }
    public int ReservationCode { get; set; } //ändra i core så den hittar reservation på koden och sätter reservaitonen då!
    public MovieViewModel MovieViewModel { get; set; }

    public ReviewViewModel() { }

    public ReviewViewModel(
        int rating,
        string comment,
        int reservationCode,
        MovieViewModel movieViewModel
    )
    {
        Rating = rating;
        Comment = comment;
        ReservationCode = reservationCode;
        MovieViewModel = movieViewModel;
    }
}
