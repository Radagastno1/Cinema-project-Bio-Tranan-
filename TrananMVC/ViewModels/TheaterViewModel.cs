using Newtonsoft.Json;
namespace TrananMVC.ViewModel;

public class TheaterViewModel
{
    public int TheaterId { get; set; }
    public string Name { get; set; }
    public int Rows { get; set; }
    public List<SeatViewModel> Seats { get; set; }

    [JsonIgnore]
    public List<MovieScreeningViewModel> MovieScreenings;
    public TheaterViewModel(){}
    public TheaterViewModel(int theaterId, string name, int rows, List<SeatViewModel> seats)
    {
        TheaterId = theaterId;
        Name = name;
        Rows = rows;
        Seats = seats;
    }
}
