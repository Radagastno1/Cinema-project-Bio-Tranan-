using TrananAPI.Interface;

namespace TrananAPI.DTO;

public class MovieDTO : IDTO
{
    public int Id { get; set; }
    public string Title { get; set; }

    public int ReleaseYear { get; set; }

    public string Language { get; set; }
    public string Description { get; set; }

    public int DurationMinutes { get; set; }
    public int MaxScreenings { get; set; }
    public int AmountOfScreenings { get; set; }
    public string ImageUrl { get; set; }
    public decimal Price { get; set; }
    public string TrailerId{get;set;}
    public List<ActorDTO> ActorDTOs { get; set; }
    public List<DirectorDTO> DirectorDTOs { get; set; }

    public MovieDTO(
        int movieId,
        string title,
        int releaseYear,
        string language,
        int durationMinutes,
        string description,
        int amountOfScreenings,
        int maxScreenings,
        string imageUrl,
        List<ActorDTO> actorDTOs,
        List<DirectorDTO> directorDTOs,
        decimal price, string trailerId
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
        ActorDTOs = actorDTOs;
        DirectorDTOs = directorDTOs;
        Price = price;
        TrailerId = trailerId;
    }
}
