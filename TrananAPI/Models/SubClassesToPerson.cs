using Newtonsoft.Json;
namespace TrananAPI.Models;

public class Actor
{ //väljer att ha typen actor för underållning i db samt kunna föra statistik om vilka skådisar som drog mest kunder osv..
    public int ActorId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [JsonIgnore]
    public List<Movie> Movies { get; set; }
    public int Statistic;
    public Actor(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}
public class Director 
{
    public int DirectorId{get;set;}
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [JsonIgnore]
    public List<Movie> Movies { get; set; }

    public Director(string firstName, string lastName)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
    }
}
public class Customer : Person
{
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public List<Reservation> Reservations { get; set; }

    public Customer(string firstName, string lastName)
        : base(firstName, lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}

public class Admin : Person
{
    public string Password { get; set; }

    public Admin(string firstName, string lastName)
        : base(firstName, lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}
