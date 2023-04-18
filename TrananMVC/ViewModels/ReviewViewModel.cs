using TrananMVC.Interface;

namespace TrananMVC.ViewModel;

public class ReviewViewModel : IViewModel
{
    public int Id{get;set;}
    public string Alias{get;set;}
    public int Rating { get; set; }
    public string Comment { get; set; }
    public int ReservationCode { get; set; } 
    public MovieViewModel MovieViewModel { get; set; }
    public int MovieViewModelId { get; set; }

    public ReviewViewModel() { }

    public ReviewViewModel(
        int rating,
        string comment,
        int reservationCode,
         string alias
    )
    {
        Rating = rating;
        Comment = comment;
        ReservationCode = reservationCode;
        Alias = alias;
    }
}
