using Newtonsoft.Json;
namespace TrananAPI.DTO;

public class TheaterDTO
{
    public int TheaterId{get;set;}
    public string Name { get; set; }
    public int Rows { get; set; }
    public List<SeatDTO> SeatDTOs { get; set; }
    [JsonIgnore]
    public List<MovieScreeningOutgoingDTO> MovieScreeningsDTOs;

    public TheaterDTO(int theaterId, string name, int rows, List<SeatDTO>seatDTOs)
    {
        TheaterId = theaterId;
        Name = name;
        Rows = rows;
        SeatDTOs = seatDTOs;
    }
}
