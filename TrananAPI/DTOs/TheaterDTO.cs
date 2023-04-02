using Newtonsoft.Json;
namespace TrananAPI.DTO;

public class TheaterDTO
{
    public string Name { get; set; }
    public int Rows { get; set; }
    public List<SeatDTO> SeatDTOs { get; set; }
    [JsonIgnore]
    public List<MovieScreeningOutgoingDTO> MovieScreeningsDTOs;

    public TheaterDTO(string name, int rows, List<SeatDTO>seatDTOs)
    {
        Name = name;
        Rows = rows;
        SeatDTOs = seatDTOs;
    }
}
