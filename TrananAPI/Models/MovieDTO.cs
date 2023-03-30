using System.ComponentModel.DataAnnotations;
namespace TrananAPI.Models.DTO;

public class MovieDTO
{
    [Required]
    public int MovieId { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public int ReleaseYear { get; set; }

    [Required]
    public string Language { get; set; }

    [Required]
    public int AmountOfScreenings { get; set; }

    [Required]
    public int MaxScreenings { get; set; }

    [Required]
    public int DurationSeconds { get; set; }
    [Required]
    public List<string> ActorNames { get; set; }

    public MovieDTO(int movieId, string title, int releaseYear, string language, int amountOfScreenings, int maxScreenings, int durationSeconds, List<string>actorNames)
    {
        MovieId = movieId;
        Title = title;
        ReleaseYear = releaseYear;
        Language = language;
        AmountOfScreenings = amountOfScreenings;
        MaxScreenings = maxScreenings;
        DurationSeconds = durationSeconds;
        ActorNames = actorNames;
    }
}
