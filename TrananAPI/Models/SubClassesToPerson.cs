namespace TrananAPI.Models;

public class Actor : Person { }

public class Director : Person { }

public class Customer : Person
{
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}

public class Admin : Person { }
