using Newtonsoft.Json;

namespace TrananMVC.Models;

public class MovieScreening
{
    public int MovieScreeningId { get; set; }
    public DateTime DateAndTime { get; set; }

    [JsonProperty(PropertyName = "MovieDTO")]
    public Movie Movie { get; set; }

    [JsonProperty(PropertyName = "TheaterDTO")]
    public Theater Theater { get; set; }

    public MovieScreening() { }

    // [JsonConstructor]
    public MovieScreening(int movieScreeningId, DateTime dateAndTime, Movie movie, Theater theater)
    {
        MovieScreeningId = movieScreeningId;
        DateAndTime = dateAndTime;
        Movie = movie;
        Theater = theater;
    }
}
