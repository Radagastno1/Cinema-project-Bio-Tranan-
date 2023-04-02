namespace TrananAPI.DTO;

public class SeatDTO
{
    public int SeatNumber { get; set; }
    public int Row { get; set; }
    public bool IsWheelChairSpace { get; set; }
    public bool IsBooked { get; set; }
    public bool IsNotBookable { get; set; }

    public SeatDTO() { }

    public SeatDTO(int seatNumber, int row)
    {
        SeatNumber = seatNumber;
        Row = row;
    }
}