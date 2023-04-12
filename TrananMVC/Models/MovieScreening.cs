using Newtonsoft.Json;

namespace TrananMVC.Models;

public class MovieScreening
{
    public int MovieScreeningId { get; set; }
    public DateTime DateAndTime { get; set; }

    [JsonProperty(PropertyName = "MovieDTO")]
    public Movie Movie { get; set; }
    public string TheaterName { get; set; }

    [JsonProperty(PropertyName = "AllSeatDTOs")]
    public List<Seat> AllSeats { get; set; }

    public MovieScreening() { }

    public MovieScreening(int movieScreeningId, DateTime dateAndTime, Movie movie, string theaterName, List<Seat> allSeats)
    {
        MovieScreeningId = movieScreeningId;
        DateAndTime = dateAndTime;
        Movie = movie;
        TheaterName = theaterName;
        AllSeats = allSeats;
    }
}
