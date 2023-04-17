using TrananAPI.Interface;

namespace TrananAPI.DTO;

public class MovieScreeningIncomingDTO : IDTO
{
    public int Id{get;set;}
    public DateTime DateAndTime { get; set; }
    public int MovieId { get; set; }

    public int TheaterId { get; set; }

    public MovieScreeningIncomingDTO() { }

    public MovieScreeningIncomingDTO(DateTime dateAndTime, int movieId, int theaterId)
    {
        DateAndTime = dateAndTime;
        MovieId = movieId;
        TheaterId = theaterId;
    }
}
