using System.ComponentModel.DataAnnotations;

namespace TrananAPI.Models.DTO;

public class MovieScreeningDTO
{
   
    public int MovieScreeningId { get; set; }

    
    public DateTime DateAndTime { get; set; }

   
    public string MovieTitle { get; set; }


    public string TheaterName { get; set; }

    public List<string> ActorNames { get; set; }

   
    public List<Seat> Seats { get; set; }

    public MovieScreeningDTO() { }

    public MovieScreeningDTO(
        DateTime dateAndTime,
        string movieTitle,
        string theaterName,
        List<string> actorNames,
        List<Seat> seats
    )
    {
        DateAndTime = dateAndTime;
        MovieTitle = movieTitle;
        TheaterName = theaterName;
        ActorNames = actorNames;
        Seats = seats;
    }
}
