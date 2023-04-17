using TrananAPI.Interface;

namespace TrananAPI.DTO;

public class DirectorDTO : IDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public DirectorDTO(int directorId, string firstName, string lastName)
    {
        Id = directorId;
        FirstName = firstName;
        LastName = lastName;
    }
}
