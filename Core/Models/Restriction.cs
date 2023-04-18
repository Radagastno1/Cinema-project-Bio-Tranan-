namespace Core.Models;

public class Restriction
{
    public int RestrictionId { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public List<Seat> Seats{get;set;} 
    public Restriction(){}
}
