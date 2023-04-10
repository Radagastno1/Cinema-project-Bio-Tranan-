namespace TrananMVC.ViewModel;

public class DirectorViewModel
{
     public int DirectorId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public DirectorViewModel(int directorId, string firstName, string lastName)
    {
        DirectorId = directorId;
        FirstName = firstName;
        LastName = lastName;
    }
}