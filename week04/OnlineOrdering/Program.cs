// Program.cs — Online Ordering (CSE 210 W04: Encapsulation)

// ── Order 1: US customer ──────────────────────────────────
Address address1 = new Address("742 Evergreen Terrace", "Springfield", "IL", "USA");
Customer customer1 = new Customer("Emeka Obi", address1);

Order order1 = new Order(customer1);
order1.AddProduct(new Product("Wireless Mouse",      "WM-204",  29.99, 2));
order1.AddProduct(new Product("USB-C Hub",           "UC-089",  49.99, 1));
order1.AddProduct(new Product("Mechanical Keyboard", "MK-512",  89.99, 1));

// ── Order 2: International customer ──────────────────────
Address address2 = new Address("14 Marina Road", "Lagos", "Lagos State", "Nigeria");
Customer customer2 = new Customer("Amina Yusuf", address2);

Order order2 = new Order(customer2);
order2.AddProduct(new Product("Laptop Stand",   "LS-301", 39.99, 1));
order2.AddProduct(new Product("Webcam HD 1080", "WC-117", 79.99, 2));

// ── Order 3: US customer ──────────────────────────────────
Address address3 = new Address("330 Oak Avenue", "Austin", "TX", "USA");
Customer customer3 = new Customer("Tunde Adewale", address3);

Order order3 = new Order(customer3);
order3.AddProduct(new Product("Monitor 27in",  "MN-720", 329.99, 1));
order3.AddProduct(new Product("HDMI Cable 2m", "HD-002",   9.99, 3));
order3.AddProduct(new Product("Cable Organizer","CO-055",  14.99, 2));

// ── Display all orders ────────────────────────────────────
List<Order> orders = new List<Order> { order1, order2, order3 };

foreach (Order order in orders)
{
    Console.WriteLine("========================================");
    Console.WriteLine(order.GetPackingLabel());
    Console.WriteLine();
    Console.WriteLine(order.GetShippingLabel());
    Console.WriteLine();
    Console.WriteLine($"Total Price: ${order.GetTotalCost():F2}");
    Console.WriteLine();
}