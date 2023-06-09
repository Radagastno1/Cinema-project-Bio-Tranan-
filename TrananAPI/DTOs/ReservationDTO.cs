using TrananAPI.Interface;

namespace TrananAPI.DTO;

public class ReservationDTO : IDTO
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public int ReservationCode { get; set; }
    public int MovieScreeningId { get; set; }
    public CustomerDTO CustomerDTO { get; set; }
    public List<int> SeatIds { get; set; }
    public bool IsCheckedIn{get;set;} 
    public ReservationDTO(
        int reservationId,
        decimal price,
        int movieScreeningId,
        CustomerDTO customerDTO,
        List<int> seatIds, int reservationCode, bool isCheckedIn
    )
    {
        Id = reservationId;
        Price = price;
        MovieScreeningId = movieScreeningId;
        CustomerDTO = customerDTO;
        SeatIds = seatIds;
        ReservationCode = reservationCode;
        IsCheckedIn = isCheckedIn;
    }
    public ReservationDTO(){}
}
