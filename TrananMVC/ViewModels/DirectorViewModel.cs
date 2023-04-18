using TrananMVC.Interface;

namespace TrananMVC.ViewModel;

public class DirectorViewModel : IViewModel
{
     public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public DirectorViewModel(int directorId, string firstName, string lastName)
    {
        Id = directorId;
        FirstName = firstName;
        LastName = lastName;
    }
}