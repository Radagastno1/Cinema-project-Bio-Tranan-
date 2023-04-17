using TrananAPI.Interface;

namespace TrananAPI.DTO;

public class SeatDTO : IDTO
{
    public int Id{get;set;}
    public int SeatNumber { get; set; }
    public int Row { get; set; }
    public bool IsWheelChairSpace { get; set; }
    public bool IsBooked { get; set; }
    public bool IsNotBookable { get; set; }

    public SeatDTO() { }

    public SeatDTO(int seatId, int seatNumber, int row, bool isBooked, bool isNotBookable, bool isWheelChairSpace)
    {
        Id = seatId;
        SeatNumber = seatNumber;
        Row = row;
        IsBooked = isBooked;
        IsNotBookable = isNotBookable;
        IsWheelChairSpace = isWheelChairSpace;
    }
}
