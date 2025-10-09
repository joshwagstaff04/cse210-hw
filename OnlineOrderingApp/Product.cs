namespace OnlineOrderingApp;

public class Product
{
    private string _name;
    private string _productId;
    private decimal _pricePerUnit;
    private int _quantity;

    public Product(string name, string productId, decimal pricePerUnit, int quantity)
    {
        _name = name;
        _productId = productId;
        _pricePerUnit = pricePerUnit;
        _quantity = quantity;
    }

    // Getters / Setters
    public string GetName() => _name;
    public void SetName(string value) => _name = value;

    public string GetProductId() => _productId;
    public void SetProductId(string value) => _productId = value;

    public decimal GetPricePerUnit() => _pricePerUnit;
    public void SetPricePerUnit(decimal value) => _pricePerUnit = value;

    public int GetQuantity() => _quantity;
    public void SetQuantity(int value) => _quantity = value;

    public decimal GetTotalPrice() => _pricePerUnit * _quantity;
}
