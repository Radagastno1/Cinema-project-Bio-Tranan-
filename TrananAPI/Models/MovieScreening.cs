namespace TrananAPI.Models;

public class MovieScreening
{
    //DENNA BEHÖVS JU INTE, RÄCKER MED EN DTO
    public int MovieScreeningId { get; set; }
    public DateTime DateAndTime { get; set; }
    public int MovieId { get; set; }
    public Movie Movie { get; set; }
    public int TheaterId { get; set; }
    public Theater Theater { get; set; }
    // public List<Reservation> Reservations { get; set; }

    public MovieScreening(){}
    public MovieScreening(int movieScreeningId, DateTime dateAndTime, Movie movie, Theater theater)
    {
        MovieScreeningId = movieScreeningId;
        DateAndTime = dateAndTime;
        Movie = movie;
        Theater = theater;
    }
}
