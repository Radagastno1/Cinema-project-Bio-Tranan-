using TrananAPI.Interface;

namespace TrananAPI.DTO;

public class ActorDTO : IDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public ActorDTO(int actorId, string firstName, string lastName)
    {
        Id = actorId;
        FirstName = firstName;
        LastName = lastName;
    }
}
