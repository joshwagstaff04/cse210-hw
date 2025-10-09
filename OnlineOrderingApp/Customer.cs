namespace OnlineOrderingApp;

public class Customer
{
    private string _name;
    private Address _address;

    public Customer(string name, Address address)
    {
        _name = name;
        _address = address;
    }

    
    public string GetName() => _name;
    public void SetName(string value) => _name = value;

    public Address GetAddress() => _address;
    public void SetAddress(Address value) => _address = value;

    public bool LivesInUSA() => _address.IsInUSA();
}

