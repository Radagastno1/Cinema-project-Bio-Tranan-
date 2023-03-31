namespace TrananAPI.DTO;

public class ActorDTO 
{
    public int ActorId{get;set;}
    public string FirstName{get;set;}
    public string LastName{get;set;}
    public ActorDTO(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}