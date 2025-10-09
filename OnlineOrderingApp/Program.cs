using OnlineOrderingApp;

class Program
{
    static void Main(string[] args)
    {
        // Addresses
        var addrUSA = new Address("123 Main St", "Provo", "UT", "USA");
        var addrUK  = new Address("221B Baker Street", "London", "", "UK");

        // Customers
        var custAlice = new Customer("Alice Johnson", addrUSA);
        var custSherlock = new Customer("Sherlock Holmes", addrUK);

        // Products
        var cable = new Product("USB-C Cable", "UC-1001", 9.99m, 2);
        var mouse = new Product("Wireless Mouse", "WM-2002", 24.50m, 1);
        var keyboard = new Product("Mechanical Keyboard", "MK-3003", 79.00m, 1);
        var pad = new Product("Mouse Pad", "MP-4004", 7.50m, 3);

        // Order 1 (USA)
        var order1 = new Order(custAlice);
        order1.AddProduct(cable);
        order1.AddProduct(mouse);

        // Order 2 (International)
        var order2 = new Order(custSherlock);
        order2.AddProduct(keyboard);
        order2.AddProduct(pad);

        // Display results
        PrintOrder("ORDER 1 (USA)", order1);
        PrintOrder("ORDER 2 (International)", order2);
    }

    static void PrintOrder(string title, Order order)
    {
        Console.WriteLine($"=== {title} ===");
        Console.WriteLine(order.GetPackingLabel());
        Console.WriteLine(order.GetShippingLabel());
        Console.WriteLine($"Total: ${order.GetTotalCost():0.00}");
        Console.WriteLine();
    }
}
