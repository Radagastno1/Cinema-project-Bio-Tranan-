using System.ComponentModel.DataAnnotations;
namespace TrananAPI.DTO;

public class MovieDTO
{
    public int MovieId{get;set;}
    public string Title { get; set; }

    public int ReleaseYear { get; set; }

    public string Language { get; set; }
    public string Description{get;set;}
 
    public int DurationSeconds { get; set; }
    public int MaxScreenings{get;set;}
    public int AmountOfScreenings {get;set;}
  
    public List<ActorDTO> ActorDTOs { get; set; }
    public List<DirectorDTO> DirectorDTOs { get; set; }

    public MovieDTO(int movieId, string title, int releaseYear, string language, int durationSeconds,string description, int amountOfScreenings, int maxScreenings, List<ActorDTO>actorDTOs, List<DirectorDTO>directorDTOs)
    {
        MovieId = movieId;
        Title = title;
        ReleaseYear = releaseYear;
        Language = language;
        DurationSeconds = durationSeconds;
        Description = description;
        AmountOfScreenings = amountOfScreenings;
        MaxScreenings = maxScreenings;
        ActorDTOs = actorDTOs;
        DirectorDTOs = directorDTOs;
    }
}
