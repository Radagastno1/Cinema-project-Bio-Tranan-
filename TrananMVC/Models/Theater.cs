using Newtonsoft.Json;
namespace TrananMVC.Models;

public class Theater
{
    public int TheaterId { get; set; }
    public string Name { get; set; }
    public int Rows { get; set; }
    public List<Seat> Seats { get; set; }

    [JsonIgnore]
    public List<MovieScreening> MovieScreenings;

    public Theater(int theaterId, string name, int rows, List<Seat> seats)
    {
        TheaterId = theaterId;
        Name = name;
        Rows = rows;
        Seats = seats;
    }
}
