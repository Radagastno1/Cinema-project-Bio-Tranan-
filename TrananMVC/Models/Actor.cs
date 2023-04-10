namespace TrananMVC.Models;

public class Actor : Person
{
    public int ActorId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public Actor(int actorId, string firstName, string lastName)
    {
        ActorId = actorId;
        FirstName = firstName;
        LastName = lastName;
    }
}
