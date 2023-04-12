namespace TrananMVC.ViewModel;

public class ReservationViewModel
{
    public int ReservationId { get; set; }
    public decimal Price { get; set; }
    public int ReservationCode { get; set; }
    public int MovieScreeningId { get; set; }
    public CustomerViewModel CustomerViewModel { get; set; } = new();
    public List<int> SeatIds { get; set; } = new();
}