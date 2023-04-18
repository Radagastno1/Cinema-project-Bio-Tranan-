using TrananMVC.Interface;

namespace TrananMVC.ViewModel;

public class ActorViewModel : IViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public ActorViewModel(int actorId, string firstName, string lastName)
    {
        Id = actorId;
        FirstName = firstName;
        LastName = lastName;
    }
}