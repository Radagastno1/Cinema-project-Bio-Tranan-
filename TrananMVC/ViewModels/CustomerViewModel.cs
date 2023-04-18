using TrananMVC.Interface;

namespace TrananMVC.ViewModel;

public class CustomerViewModel : IViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public CustomerViewModel(){}
    public CustomerViewModel(int customerId, string firstName, string lastName, string phoneNumber, string email)
    {
        Id = customerId;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Email = email;
    }
}
