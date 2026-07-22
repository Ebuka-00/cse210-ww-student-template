public class Order
{
    private List<Product> _products;
    private Customer _customer;

    private const double USA_SHIPPING     = 5.00;
    private const double FOREIGN_SHIPPING = 35.00;

    public Order(Customer customer)
    {
        _customer = customer;
        _products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public double GetTotalCost()
    {
        double total = _customer.LivesInUSA() ? USA_SHIPPING : FOREIGN_SHIPPING;
        foreach (Product p in _products)
            total += p.GetTotalCost();
        return total;
    }

    public string GetPackingLabel()
    {
        string label = "--- Packing Label ---\n";
        foreach (Product p in _products)
            label += $"  {p.GetName()} (ID: {p.GetProductId()})\n";
        return label.TrimEnd();
    }

    public string GetShippingLabel()
    {
        return $"--- Shipping Label ---\n{_customer.GetShippingAddress()}";
    }
}