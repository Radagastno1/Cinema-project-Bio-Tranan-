using TrananAPI.Interface;

namespace TrananAPI.DTO;

public class MovieScreeningOutgoingDTO : IDTO
{
    public int Id { get; set; }

    public DateTime DateAndTime { get; set; }
    public MovieDTO MovieDTO { get; set; }
    public decimal PricePerPerson { get; set; }
    public string TheaterName { get; set; }
    public List<SeatDTO> AllSeatDTOs { get; set; }

    public MovieScreeningOutgoingDTO() { }

    public MovieScreeningOutgoingDTO(
        int movieScreeningId,
        DateTime dateAndTime,
        MovieDTO movieDTO,
        string theaterName,
        List<SeatDTO> allSeats,
        decimal pricePerPerson
    )
    {
        Id = movieScreeningId;
        DateAndTime = dateAndTime;
        MovieDTO = movieDTO;
        TheaterName = theaterName;
        AllSeatDTOs = allSeats;
        PricePerPerson = pricePerPerson;
    }
}
