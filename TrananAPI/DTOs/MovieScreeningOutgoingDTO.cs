using System.ComponentModel.DataAnnotations;

namespace TrananAPI.DTO;

public class MovieScreeningOutgoingDTO
{
    public int MovieScreeningId { get; set; }

    public DateTime DateAndTime { get; set; }
    public MovieDTO MovieDTO { get; set; }

    // public TheaterDTO TheaterDTO { get; set; }
    public string TheaterName { get; set; }
    public List<SeatDTO> AllSeatDTOs { get; set; }

    public MovieScreeningOutgoingDTO() { }

    public MovieScreeningOutgoingDTO(
        int movieScreeningId,
        DateTime dateAndTime,
        MovieDTO movieDTO,
        string theaterName,
        List<SeatDTO> allSeats
    )
    {
        MovieScreeningId = movieScreeningId;
        DateAndTime = dateAndTime;
        MovieDTO = movieDTO;
        // TheaterDTO = theaterDTO;
        TheaterName = theaterName;
        AllSeatDTOs = allSeats;
    }
}
