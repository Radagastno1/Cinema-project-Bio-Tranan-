namespace TrananMVC.Models;

public class MovieScreening
{
    public int MovieScreeningId { get; set; }
    public DateTime DateAndTime { get; set; }
    public Movie Movie { get; set; } //outgoiing
    public int MovieId { get; set; } //incommiing
    public int TheaterId { get; set; } //incoming
    public Theater Theater { get; set; } //outgoing

    public MovieScreening() { }

    public MovieScreening(
        DateTime dateAndTime,
        int movieId,
        Movie movie,
        int theaterId,
        Theater theater
    )
    {
        DateAndTime = dateAndTime;
        MovieId = movieId;
        Movie = movie;
        TheaterId = theaterId;
        Theater = theater;
    }
}
