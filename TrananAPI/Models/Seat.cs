namespace TrananAPI.Models;

public class Seat
{
    private int Id { get; set; }
    public int SeatNumber { get; set; }
    public int Row{get;set;}
    public bool IsWheelChairSpace { get; set; }
    public bool IsBooked { get; set; }
    public bool IsNotBookable { get; set; }
    public int TheaterId { get; set; }
    public Theater Theater;
    public List<Reservation>Reservations{get;set;}
}
