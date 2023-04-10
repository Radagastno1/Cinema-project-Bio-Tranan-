namespace TrananMVC.ViewModel;

public class ActorViewModel
{
    public int ActorId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public ActorViewModel(int actorId, string firstName, string lastName)
    {
        ActorId = actorId;
        FirstName = firstName;
        LastName = lastName;
    }
}