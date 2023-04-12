using Newtonsoft.Json;
namespace TrananMVC.Models;

public class Reservation
{
    public int ReservationId { get; set; }
    public decimal Price { get; set; }
    public int ReservationCode { get; set; }
    public int MovieScreeningId { get; set; }
     [JsonProperty(PropertyName = "CustomerDTO")]
    public Customer Customer { get; set; }
    public List<int> SeatIds{get;set;}
}

