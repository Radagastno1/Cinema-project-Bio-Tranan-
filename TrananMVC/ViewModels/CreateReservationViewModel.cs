namespace TrananMVC.ViewModel;

public class CreateReservationViewModel
{
   public ReservationViewModel ReservationViewModel{get;set;} = new();
   public MovieScreeningViewModel MovieScreeningViewModel{get;set;} = new();
}
