using System.ComponentModel.DataAnnotations;
namespace TrananAPI.Models;

public class Movie
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
    // public List<Director> Directors { get; set; }
    // public List<Actor> Actors { get; set; }
    // public List<MovieScreening> MovieScreenings { get; set; }

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
