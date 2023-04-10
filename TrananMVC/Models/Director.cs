namespace TrananMVC.Models;

public class Director : Person
{
    public int DirectorId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public Director(int directorId, string firstName, string lastName)
    {
        DirectorId = directorId;
        FirstName = firstName;
        LastName = lastName;
    }
}
