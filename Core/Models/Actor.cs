using Newtonsoft.Json;

namespace Core.Models;

public class Actor
{   //man kan kolla statistik, tex vilka actors som folk såg mest på under ett år osv
    public int ActorId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [JsonIgnore]
    public List<Movie> Movies { get; set; }

    public Actor(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
    public Actor(){}
}
