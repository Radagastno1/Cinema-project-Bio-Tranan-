using Newtonsoft.Json;

namespace Core.Models;
public class Director
{
    public int DirectorId { get; set; }
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