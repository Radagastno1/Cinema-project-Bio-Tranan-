namespace TrananAPI.Models;

public class Person
{
    internal int Id { get; set; }
    internal string FirstName { get; set; }
    internal string LastName { get; set; }
    public Person(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}
