using System.ComponentModel.DataAnnotations;

namespace TrananAPI.DTO;

public class MovieScreeningOutgoingDTO
{
   
    public int MovieScreeningId { get; set; }

    public DateTime DateAndTime { get; set; }
    public MovieDTO MovieDTO{get;set;}

    public TheaterDTO TheaterDTO{get;set;}

    public MovieScreeningOutgoingDTO() { }

    public MovieScreeningOutgoingDTO(
        int movieScreeningId,
        DateTime dateAndTime,
        MovieDTO movieDTO, TheaterDTO theaterDTO  
    )
    {
        MovieScreeningId = movieScreeningId;
        DateAndTime = dateAndTime;
        MovieDTO = movieDTO;
        TheaterDTO = theaterDTO;
    }
}
