namespace TrananMVC.ViewModel;

public class ReservationViewModel
{
    public int ReservationId { get; set; }
    public decimal Price { get; set; }
    public int ReservationCode { get; set; }
    public int MovieScreeningId { get; set; }
    public CustomerViewModel CustomerViewModel { get; set; } = new();
    public List<int> SeatIds { get; set; } = new();
    public bool IsCheckedIn{get;set;}

    public ReservationViewModel() { }

    public ReservationViewModel(
        int reservationId,
        decimal price,
        int reservationCode,
        int movieScreeningId,
        CustomerViewModel customerViewModel,
        List<int> seatIds
    )
    {
        ReservationId = reservationId;
        Price = price;
        ReservationCode = reservationCode;
        MovieScreeningId = movieScreeningId;
        CustomerViewModel = customerViewModel;
        SeatIds = seatIds;
    }
}
