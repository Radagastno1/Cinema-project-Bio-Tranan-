using Newtonsoft.Json;
using TrananMVC.Interface;

namespace TrananMVC.ViewModel;

public class TheaterViewModel : IViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Rows { get; set; }
    public List<SeatViewModel> Seats { get; set; }

    [JsonIgnore]
    public List<MovieScreeningViewModel> MovieScreenings;

    public TheaterViewModel() { }

    public TheaterViewModel(int theaterId, string name, int rows, List<SeatViewModel> seats)
    {
        Id = theaterId;
        Name = name;
        Rows = rows;
        Seats = seats;
    }
}
