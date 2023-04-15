namespace TrananMVC.ViewModel;

public class MovieScreeningReservationViewModel
{
   public ReservationViewModel ReservationViewModel{get;set;} = new();
   public MovieScreeningViewModel MovieScreeningViewModel{get;set;} = new();
}
