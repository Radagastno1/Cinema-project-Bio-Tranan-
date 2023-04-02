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
    public Theater(string name, int rows, List<Seat> seats)
    {
        Name = name;
        Seats = seats;
    }
}
