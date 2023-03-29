namespace TrananAPI.Models;

public class Reservation
{
    private int Id { get; set; }
    public decimal Price { get; set; }
    public int ReservationCode { get; set; }
    public int MovieScreeningId { get; set; }
    public MovieScreening MovieScreening { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
}
