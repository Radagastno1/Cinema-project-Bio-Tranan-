namespace TrananAPI.Models;

public class Theater
{
    public int TheaterId { get; set; }
    public string Name { get; set; }
    public int Rows { get; set; }
    public int MaxAmountAvailebleSeats{get;set;}
    public List<Seat> Seats { get; set; }
    public List<MovieScreening> MovieScreenings { get; set; }
    public Theater(){}
    public Theater(int theaterId, string name, int rows, List<Seat> seats)
    {
        TheaterId = theaterId;
        Name = name;
        Rows = rows;
        Seats = seats;
    }
}
