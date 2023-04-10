namespace TrananMVC.ViewModel;

public class MovieViewModel
{
    public int MovieId { get; set; }
    public string Title { get; set; }

    public int ReleaseYear { get; set; }

    public string Language { get; set; }
    public string Description { get; set; }

    public int DurationMinutes { get; set; }
    public int MaxScreenings { get; set; }
    public int AmountOfScreenings { get; set; }

    public List<ActorViewModel> Actors { get; set; }
    public List<DirectorViewModel> Directors { get; set; }

    public MovieViewModel(
        int movieId,
        string title,
        int releaseYear,
        string language,
        int durationMinutes,
        string description,
        int amountOfScreenings,
        int maxScreenings,
        List<ActorViewModel> actors,
        List<DirectorViewModel> directors
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
