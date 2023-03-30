namespace TrananAPI.Models;

public class MovieScreening
{
    private int Id { get; set; }
    public DateOnly Date { get; set; }
    public int MovieId { get; set; }
    public Movie Movie { get; set; }
    public int TheaterId { get; set; }
    public Theater Theater { get; set; }
    public List<Reservation> Reservations { get; set; }
}
