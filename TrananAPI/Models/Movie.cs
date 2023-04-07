using System.ComponentModel.DataAnnotations;
namespace TrananAPI.Models;

public class Movie
{

    public int MovieId { get; set; }

    public string Title { get; set; }
  
    public int ReleaseYear { get; set; }
  
    public string Language { get; set; }
    public string Description{get;set;}

    public int AmountOfScreenings { get; set; }

    public int MaxScreenings { get; set; }
 
    public int DurationSeconds { get; set; }
    public List<Director> Directors { get; set; }
    public List<Actor> Actors { get; set; }
    public List<MovieScreening> MovieScreenings;

     public Movie(
        string title,
        int releaseYear,
        string language, int durationSeconds, string description, List<Actor>actors
    )
    {
        Title = title;
        ReleaseYear = releaseYear;
        Language = language;
        DurationSeconds = durationSeconds;
        Description = description;
        Actors = actors;
    }
    public Movie(){}
}
