namespace Core.Models;

public class MovieScreening
{
    public int MovieScreeningId { get; set; }
    public DateTime DateAndTime { get; set; }
    public int MovieId { get; set; }
    public decimal PricePerPerson{get;set;}
    public Movie Movie { get; set; }
    public int TheaterId { get; set; }
    public Theater Theater { get; set; }
    public List<Reservation> Reservations { get; set; }
    public List<Seat> ReservedSeats { get; set; } = new();

    public MovieScreening() { }

    public MovieScreening(DateTime dateAndTime, Movie movie, Theater theater)
    {
        DateAndTime = dateAndTime;
        Movie = movie;
        Theater = theater;
    }
}
