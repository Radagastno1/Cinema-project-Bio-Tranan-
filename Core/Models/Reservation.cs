namespace Core.Models;

public class Reservation
{
    public int ReservationId { get; set; }
    public decimal Price { get; set; }
    public int ReservationCode { get; set; }
    public int MovieScreeningId { get; set; }
    public MovieScreening MovieScreening { get; set; } 
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public List<Seat> Seats { get; set; }
    public bool IsCheckedIn{get;set;} 

    public Reservation() { }
}
