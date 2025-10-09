namespace OnlineOrderingApp;

public class Address
{
    private string _street;
    private string _city;
    private string _stateOrProvince;
    private string _country;

    public Address(string street, string city, string stateOrProvince, string country)
    {
        _street = street;
        _city = city;
        _stateOrProvince = stateOrProvince;
        _country = country;
    }

    
    public string GetStreet() => _street;
    public void SetStreet(string value) => _street = value;

    public string GetCity() => _city;
    public void SetCity(string value) => _city = value;

    public string GetStateOrProvince() => _stateOrProvince;
    public void SetStateOrProvince(string value) => _stateOrProvince = value;

    public string GetCountry() => _country;
    public void SetCountry(string value) => _country = value;

    public bool IsInUSA() => _country.Trim().ToUpperInvariant() == "USA";

    public string ToLabelString()
    {
        
        if (string.IsNullOrWhiteSpace(_stateOrProvince))
            return $"{_street}\n{_city}\n{_country}";
        return $"{_street}\n{_city}, {_stateOrProvince}\n{_country}";
    }
}
