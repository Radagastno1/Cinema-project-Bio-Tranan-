using System.ComponentModel.DataAnnotations;
namespace TrananAPI.DTO;

public class MovieDTO
{
    public int MovieId{get;set;}
    public string Title { get; set; }

    public int ReleaseYear { get; set; }

    public string Language { get; set; }
 
    public int DurationSeconds { get; set; }
    // public int MaxScreenings{get;set;}
    // public int AmountOfScreenings {get;set;}
  
    public List<ActorDTO> ActorDTOs { get; set; }

    public MovieDTO(int movieId, string title, int releaseYear, string language, int durationSeconds, List<ActorDTO>actorDTOs)
    {
        MovieId = movieId;
        Title = title;
        ReleaseYear = releaseYear;
        Language = language;
        DurationSeconds = durationSeconds;
        ActorDTOs = actorDTOs;
    }
    private string SecondsToTimeAsString()
    {
        return "";
    }
}
