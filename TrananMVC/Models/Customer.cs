namespace TrananMVC.Models;

public class Customer : Person
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public Customer(){}

    public Customer(
        int customerId,
        string firstName,
        string lastName,
        string phoneNumber,
        string email
    )
    {
        CustomerId = customerId;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Email = email;
    }
}
