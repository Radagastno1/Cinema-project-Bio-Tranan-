namespace TrananAPI.DTO;

public class DirectorDTO
{
    public int DirectorId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public DirectorDTO(int directorId, string firstName, string lastName)
    {
        DirectorId = directorId;
        FirstName = firstName;
        LastName = lastName;
    }
}
