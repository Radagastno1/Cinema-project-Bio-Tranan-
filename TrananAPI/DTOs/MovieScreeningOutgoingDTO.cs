using System.ComponentModel.DataAnnotations;

namespace TrananAPI.DTO;

public class MovieScreeningOutgoingDTO
{
    public int MovieScreeningId { get; set; }

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
        MovieScreeningId = movieScreeningId;
        DateAndTime = dateAndTime;
        MovieDTO = movieDTO;
        TheaterName = theaterName;
        AllSeatDTOs = allSeats;
        PricePerPerson = pricePerPerson;
    }
}
