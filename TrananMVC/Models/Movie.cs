using Newtonsoft.Json;

namespace TrananMVC.Models;

public class Movie
{
    public int MovieId { get; set; }

    public string Title { get; set; }
    public int ReleaseYear { get; set; }
    public string Language { get; set; }

    public string Description { get; set; }

    public int DurationMinutes { get; set; }

    public int MaxScreenings { get; set; }

    public int AmountOfScreenings { get; set; }
    public string ImageUrl { get; set; }

    [JsonProperty(PropertyName = "ActorDTOs")]
    public List<Actor> Actors { get; set; }

    [JsonProperty(PropertyName = "DirectorDTOs")]
    public List<Director> Directors { get; set; }

    public Movie(
        int movieId,
        string title,
        int releaseYear,
        string language,
        int durationMinutes,
        string description,
        int amountOfScreenings,
        int maxScreenings,
        List<Actor> actors,
        List<Director> directors
    )
    {
        MovieId = movieId;
        Title = title;
        ReleaseYear = releaseYear;
        Language = language;
        DurationMinutes = durationMinutes;
        Description = description;
        AmountOfScreenings = amountOfScreenings;
        MaxScreenings = maxScreenings;
        Actors = actors;
        Directors = directors;
    }
}
