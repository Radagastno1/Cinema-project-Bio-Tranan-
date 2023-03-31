namespace TrananAPI.DTO;

public class SeatDTO
{
    public int SeatId { get; set; }
    public int SeatNumber { get; set; }
    public int Row { get; set; }
    public bool IsWheelChairSpace { get; set; }
    public bool IsBooked { get; set; }
    public bool IsNotBookable { get; set; }

    public SeatDTO() { }

    public SeatDTO(int seatId, int seatNumber, int row)
    {
        SeatId = seatId;
        SeatNumber = seatNumber;
        Row = row;
    }
}
