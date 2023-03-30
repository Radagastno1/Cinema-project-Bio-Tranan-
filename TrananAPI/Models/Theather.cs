namespace TrananAPI.Models;

public class Theater
{
    private int Id { get; set; }
    public string Name { get; set; }
    public int Rows { get; set; }
    public List<Seat> Seats { get; set; }
    public List<MovieScreening> MovieScreenings { get; set; }
}
