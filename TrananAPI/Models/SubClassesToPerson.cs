namespace TrananAPI.Models;

public class Actor : Person 
{  //väljer att ha typen actor för underållning i db samt kunna föra statistik om vilka skådisar som drog mest kunder osv..
    public List<Movie>Movies{get;set;}
}
public class Director : Person 
{ 
    public List<Movie>Movies{get;set;}
}
public class Customer : Person
{
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public List<Reservation>Reservations{get;set;}
}
public class Admin : Person 
{ 
    public string Password{get;set;}
}