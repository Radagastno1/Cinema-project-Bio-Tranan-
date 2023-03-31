using System.ComponentModel.DataAnnotations;

namespace TrananAPI.DTO;

public class MovieScreeningDTO
{
   
    public int MovieScreeningId { get; set; }

    public DateTime DateAndTime { get; set; }
    public int MovieId{get;set;}

    public int TheaterId{get;set;}

    public MovieScreeningDTO() { }

    public MovieScreeningDTO(
        DateTime dateAndTime,
        int movieId, int theaterId   
    )
    {
        DateAndTime = dateAndTime;
        MovieId = movieId;
        TheaterId = theaterId;
    }
}
