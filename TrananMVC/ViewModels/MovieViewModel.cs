using TrananMVC.Interface;

namespace TrananMVC.ViewModel;

public class MovieViewModel : IViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }

    public int ReleaseYear { get; set; }

    public string Language { get; set; }
    public string Description { get; set; }

    public int DurationMinutes { get; set; }
    public string DurationString{get;set;}
    public int MaxScreenings { get; set; }
    public int AmountOfScreenings { get; set; }
    public string ImageUrl { get; set; }
    public string TrailerId{get;set;}
    public string TrailerLink{get;set;}
    public List<ActorViewModel> Actors { get; set; }
    public List<DirectorViewModel> Directors { get; set; }
    public List<ReviewViewModel> Reviews{get;set;} = new();
    public MovieViewModel() { }

    public MovieViewModel(
        int movieId,
        string title,
        int releaseYear,
        string language,
        string description,
        int durationMinutes,
        int amountOfScreenings,
        int maxScreenings,
        string imageUrl,
        List<ActorViewModel> actors,
        List<DirectorViewModel> directors,
        string trailerId,
        List<ReviewViewModel> reviewViewModels
    )
    {
        Id = movieId;
        Title = title;
        ReleaseYear = releaseYear;
        Language = language;
        DurationMinutes = durationMinutes;
        Description = description;
        AmountOfScreenings = amountOfScreenings;
        MaxScreenings = maxScreenings;
        ImageUrl = imageUrl;
        Actors = actors;
        Directors = directors;
        DurationString = GenerateDurationString(DurationMinutes);
        TrailerId = trailerId;
        Reviews = reviewViewModels;
    }

    private string GenerateDurationString(int durationMinutes)
    {
        var hours = durationMinutes / 60;
        var minutes = durationMinutes % 60;
        return $"{hours} tim {minutes} min";
    }
}
