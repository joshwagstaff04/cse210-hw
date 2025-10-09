using System.Text;

namespace OnlineOrderingApp;

public class Order
{
    private readonly List<Product> _products = new();
    private readonly Customer _customer;

    public Order(Customer customer)
    {
        _customer = customer;
    }

    public void AddProduct(Product product) => _products.Add(product);

    public decimal GetTotalCost()
    {
        decimal subtotal = 0m;
        foreach (var p in _products)
        {
            subtotal += p.GetTotalPrice();
        }

        decimal shipping = _customer.LivesInUSA() ? 5m : 35m;
        return subtotal + shipping;
    }

    public string GetPackingLabel()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Packing Label:");
        foreach (var p in _products)
        {
            sb.AppendLine($" - {p.GetName()} (#{p.GetProductId()}) x{p.GetQuantity()}");
        }
        return sb.ToString().TrimEnd();
    }

    public string GetShippingLabel()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Ship To:");
        sb.AppendLine(_customer.GetName());
        sb.Append(_customer.GetAddress().ToLabelString());
        return sb.ToString();
    }
}
