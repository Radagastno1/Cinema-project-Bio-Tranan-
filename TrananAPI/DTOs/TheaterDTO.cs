using Newtonsoft.Json;
using TrananAPI.Interface;

namespace TrananAPI.DTO;

public class TheaterDTO : IDTO
{
    public int Id{get;set;}
    public string Name { get; set; }
    public decimal TheaterPrice{get;set;}
    public int Rows { get; set; }
    public List<SeatDTO> SeatDTOs { get; set; }
    [JsonIgnore]
    public List<MovieScreeningOutgoingDTO> MovieScreeningsDTOs;

    public TheaterDTO(int theaterId, string name, int rows, List<SeatDTO>seatDTOs, decimal theaterPrice)
    {
        Id = theaterId;
        Name = name;
        Rows = rows;
        SeatDTOs = seatDTOs;
        TheaterPrice = theaterPrice;
    }
}
