namespace TrananMVC.ViewModel;

public class CreatedReservationViewModel
{
    public ReservationViewModel ReservationViewModel{get;set;} 
    public DateTime DateAndTime { get; set; }
    public int MovieId { get; set; } 
    public string MovieTitle { get; set; }
    public string MovieImageUrl { get; set; }
    public string TheaterName { get; set; }

}
