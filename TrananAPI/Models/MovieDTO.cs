using System.ComponentModel.DataAnnotations;
namespace TrananAPI.Models.DTO;

public class MovieDTO
{

    public int MovieId { get; set; }


    public string Title { get; set; }

  
    public int ReleaseYear { get; set; }


    public string Language { get; set; }


    public int AmountOfScreenings { get; set; }

 
    public int MaxScreenings { get; set; }

 
    public int DurationSeconds { get; set; }
  
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
