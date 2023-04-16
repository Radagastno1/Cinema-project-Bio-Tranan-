namespace Core.Models;

public class Restriction
{
    public int RestrictionId{get;set;}
    public string Description{get;set;}
    public Theater Theater{get;set;}
    public int TheaterId{get;set;}
    public int MaxAmountSeatsFree{get;set;}
    public DateTime StartTime{get;set;}
    public DateTime EndTime{get;set;}
}