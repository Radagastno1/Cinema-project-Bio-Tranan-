namespace TrananAPI.DTO;

public class ReservationDTO
{
    public int ReservationId { get; set; }
    public decimal Price { get; set; }
    public int ReservationCode { get; set; }
    public int MovieScreeningId { get; set; }
    public CustomerDTO CustomerDTO { get; set; }
    public List<int> SeatIds { get; set; }

    public ReservationDTO(
        decimal price,
        int movieScreeningId,
        CustomerDTO customerDTO,
        List<int> seatIds, int reservationCode = 0
    )
    {
        Price = price;
        MovieScreeningId = movieScreeningId;
        CustomerDTO = customerDTO;
        SeatIds = seatIds;
        ReservationCode = reservationCode;
    }
    public ReservationDTO(){}
}
