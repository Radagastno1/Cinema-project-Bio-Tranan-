namespace TrananAPI.Models;

public class Movie
{
    private int Id { get; set; }
    public string Title { get; set; }
    public int ReleaseYear { get; set; }
    public string Language { get; set; }
    public int AmountOfScreenings { get; set; }
    public int MaxScreenings { get; set; }
    public TimeSpan Duration { get; set; }
    public List<Director> Directors { get; set; }
    public List<Actor> Actors { get; set; }
    public List<MovieScreening> MovieScreenings { get; set; }

    public Movie(
        string title,
        int releaseYear,
        string language,
        int amountOfScreenings,
        int maxScreenings
    )
    {
        Title = title;
        ReleaseYear = releaseYear;
        Language = language;
        AmountOfScreenings = amountOfScreenings;
        MaxScreenings = maxScreenings;
    }
}
