using System.ComponentModel.DataAnnotations;
namespace TrananAPI.Models;

public class Movie
{

    public int MovieId { get; set; }

    public string Title { get; set; }
  
    public int ReleaseYear { get; set; }
  
    public string Language { get; set; }

    public int AmountOfScreenings { get; set; }

    public int MaxScreenings { get; set; }
 
    public int DurationSeconds { get; set; }
    public List<Director> Directors { get; set; }
    public List<Actor> Actors { get; set; }
    public List<MovieScreening> MovieScreenings{get;set;}

     public Movie(int movieId,
        string title,
        int releaseYear,
        string language,
        int amountOfScreenings,
        int maxScreenings, List<Actor>actors
    )
    {
        MovieId = movieId;
        Title = title;
        ReleaseYear = releaseYear;
        Language = language;
        AmountOfScreenings = amountOfScreenings;
        MaxScreenings = maxScreenings;
        Actors = actors;
    }
    public Movie(){}
}
