namespace TrananAPI.DTO;

public class ActorDTO
{
    public int ActorId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public ActorDTO(int actorId, string firstName, string lastName)
    {
        ActorId = actorId;
        FirstName = firstName;
        LastName = lastName;
    }
}
