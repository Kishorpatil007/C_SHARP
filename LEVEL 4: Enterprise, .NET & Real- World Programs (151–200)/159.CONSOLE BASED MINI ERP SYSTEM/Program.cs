using System;
using System.Collections.Generic;
using System.Linq;

public class Employee
{
    public int EmployeeId { get; set; }
    public string Name { get; set; }
    public string Position { get; set; }
    public decimal Salary { get; set; }
    public DateTime JoinDate { get; set; }
    public bool IsActive { get; set; }
}

public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string Category { get; set; }
    public decimal UnitPrice { get; set; }
    public int QuantityInStock { get; set; }
    public int ReorderLevel { get; set; }
}

public class Customer
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
}

public class SalesOrder
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<SalesOrderItem> Items { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; }
}

public class SalesOrderItem
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineTotal { get; set; }
}

public class PurchaseOrder
{
    public int PurchaseOrderId { get; set; }
    public string VendorName { get; set; }
    public DateTime OrderDate { get; set; }
    public List<PurchaseOrderItem> Items { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; }
}

public class PurchaseOrderItem
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineTotal { get; set; }
}

public class ERPSystem
{
    private List<Employee> employees = new List<Employee>();
    private List<Product> products = new List<Product>();
    private List<Customer> customers = new List<Customer>();
    private List<SalesOrder> salesOrders = new List<SalesOrder>();
    private List<PurchaseOrder> purchaseOrders = new List<PurchaseOrder>();

    private int nextEmployeeId = 1;
    private int nextProductId = 1;
    private int nextCustomerId = 1;
    private int nextOrderId = 1;
    private int nextPurchaseOrderId = 1;

    public ERPSystem()
    {
        InitializeDefaultData();
    }

    private void InitializeDefaultData()
    {
        employees.Add(new Employee { EmployeeId = nextEmployeeId++, Name = "John Smith", Position = "Manager", Salary = 50000, JoinDate = DateTime.Now, IsActive = true });
        employees.Add(new Employee { EmployeeId = nextEmployeeId++, Name = "Jane Doe", Position = "Sales", Salary = 40000, JoinDate = DateTime.Now, IsActive = true });

        products.Add(new Product { ProductId = nextProductId++, ProductName = "Laptop", Category = "Electronics", UnitPrice = 800, QuantityInStock = 50, ReorderLevel = 10 });
        products.Add(new Product { ProductId = nextProductId++, ProductName = "Mouse", Category = "Accessories", UnitPrice = 25, QuantityInStock = 200, ReorderLevel = 50 });
        products.Add(new Product { ProductId = nextProductId++, ProductName = "Keyboard", Category = "Accessories", UnitPrice = 75, QuantityInStock = 150, ReorderLevel = 30 });

        customers.Add(new Customer { CustomerId = nextCustomerId++, CustomerName = "ABC Corporation", Email = "abc@company.com", Phone = "555-1234", Address = "123 Main St" });
        customers.Add(new Customer { CustomerId = nextCustomerId++, CustomerName = "XYZ Industries", Email = "xyz@company.com", Phone = "555-5678", Address = "456 Oak Ave" });
    }

    public void AddEmployee(string name, string position, decimal salary)
    {
        employees.Add(new Employee
        {
            EmployeeId = nextEmployeeId++,
            Name = name,
            Position = position,
            Salary = salary,
            JoinDate = DateTime.Now,
            IsActive = true
        });
        Console.WriteLine("Employee added successfully.");
    }

    public void DisplayEmployees()
    {
        if (employees.Count == 0)
        {
            Console.WriteLine("No employees found.");
            return;
        }

        Console.WriteLine("\n========== Employee List ==========");
        Console.WriteLine("{0,-5} {1,-20} {2,-15} {3,-12} {4,-15}", "ID", "Name", "Position", "Salary", "Status");
        Console.WriteLine(new string('-', 67));

        foreach (var emp in employees.Where(e => e.IsActive))
        {
            Console.WriteLine("{0,-5} {1,-20} {2,-15} {3,-12:C} {4,-15}", emp.EmployeeId, emp.Name, emp.Position, emp.Salary, "Active");
        }
        Console.WriteLine();
    }

    public void AddProduct(string name, string category, decimal price, int quantity, int reorderLevel)
    {
        products.Add(new Product
        {
            ProductId = nextProductId++,
            ProductName = name,
            Category = category,
            UnitPrice = price,
            QuantityInStock = quantity,
            ReorderLevel = reorderLevel
        });
        Console.WriteLine("Product added successfully.");
    }

    public void DisplayInventory()
    {
        if (products.Count == 0)
        {
            Console.WriteLine("No products found.");
            return;
        }

        Console.WriteLine("\n========== Inventory Report ==========");
        Console.WriteLine("{0,-5} {1,-20} {2,-15} {3,-12} {4,-10} {5,-10}", "ID", "Product", "Category", "Price", "Stock", "Status");
        Console.WriteLine(new string('-', 72));

        foreach (var prod in products)
        {
            string status = prod.QuantityInStock <= prod.ReorderLevel ? "LOW STOCK" : "OK";
            Console.WriteLine("{0,-5} {1,-20} {2,-15} {3,-12:C} {4,-10} {5,-10}", prod.ProductId, prod.ProductName, prod.Category, prod.UnitPrice, prod.QuantityInStock, status);
        }
        Console.WriteLine();
    }

    public void AddCustomer(string name, string email, string phone, string address)
    {
        customers.Add(new Customer
        {
            CustomerId = nextCustomerId++,
            CustomerName = name,
            Email = email,
            Phone = phone,
            Address = address
        });
        Console.WriteLine("Customer added successfully.");
    }

    public void DisplayCustomers()
    {
        if (customers.Count == 0)
        {
            Console.WriteLine("No customers found.");
            return;
        }

        Console.WriteLine("\n========== Customer List ==========");
        Console.WriteLine("{0,-5} {1,-20} {2,-25} {3,-15} {4,-30}", "ID", "Name", "Email", "Phone", "Address");
        Console.WriteLine(new string('-', 95));

        foreach (var cust in customers)
        {
            Console.WriteLine("{0,-5} {1,-20} {2,-25} {3,-15} {4,-30}", cust.CustomerId, cust.CustomerName, cust.Email, cust.Phone, cust.Address);
        }
        Console.WriteLine();
    }

    public void CreateSalesOrder(int customerId, List<(int productId, int quantity)> items)
    {
        var customer = customers.FirstOrDefault(c => c.CustomerId == customerId);
        if (customer == null)
        {
            Console.WriteLine("Customer not found.");
            return;
        }

        var order = new SalesOrder
        {
            OrderId = nextOrderId++,
            CustomerId = customerId,
            OrderDate = DateTime.Now,
            Items = new List<SalesOrderItem>(),
            Status = "Pending"
        };

        decimal totalAmount = 0;

        foreach (var item in items)
        {
            var product = products.FirstOrDefault(p => p.ProductId == item.productId);
            if (product == null)
            {
                Console.WriteLine($"Product ID {item.productId} not found.");
                return;
            }

            if (product.QuantityInStock < item.quantity)
            {
                Console.WriteLine($"Insufficient stock for {product.ProductName}.");
                return;
            }

            var orderItem = new SalesOrderItem
            {
                ProductId = item.productId,
                Quantity = item.quantity,
                UnitPrice = product.UnitPrice,
                LineTotal = product.UnitPrice * item.quantity
            };

            order.Items.Add(orderItem);
            totalAmount += orderItem.LineTotal;
            product.QuantityInStock -= item.quantity;
        }

        order.TotalAmount = totalAmount;
        salesOrders.Add(order);
        Console.WriteLine($"Sales Order #{order.OrderId} created successfully. Total: {order.TotalAmount:C}");
    }

    public void DisplaySalesOrders()
    {
        if (salesOrders.Count == 0)
        {
            Console.WriteLine("No sales orders found.");
            return;
        }

        Console.WriteLine("\n========== Sales Orders ==========");
        Console.WriteLine("{0,-8} {1,-15} {2,-15} {3,-12} {4,-15}", "Order ID", "Customer ID", "Order Date", "Total Amount", "Status");
        Console.WriteLine(new string('-', 65));

        foreach (var order in salesOrders)
        {
            Console.WriteLine("{0,-8} {1,-15} {2,-15:yyyy-MM-dd} {3,-12:C} {4,-15}", order.OrderId, order.CustomerId, order.OrderDate, order.TotalAmount, order.Status);
        }
        Console.WriteLine();
    }

    public void CreatePurchaseOrder(string vendorName, List<(int productId, int quantity, decimal unitPrice)> items)
    {
        var order = new PurchaseOrder
        {
            PurchaseOrderId = nextPurchaseOrderId++,
            VendorName = vendorName,
            OrderDate = DateTime.Now,
            Items = new List<PurchaseOrderItem>(),
            Status = "Pending"
        };

        decimal totalAmount = 0;

        foreach (var item in items)
        {
            var orderItem = new PurchaseOrderItem
            {
                ProductId = item.productId,
                Quantity = item.quantity,
                UnitPrice = item.unitPrice,
                LineTotal = item.unitPrice * item.quantity
            };

            order.Items.Add(orderItem);
            totalAmount += orderItem.LineTotal;
        }

        order.TotalAmount = totalAmount;
        purchaseOrders.Add(order);
        Console.WriteLine($"Purchase Order #{order.PurchaseOrderId} created successfully. Total: {order.TotalAmount:C}");
    }

    public void DisplayPurchaseOrders()
    {
        if (purchaseOrders.Count == 0)
        {
            Console.WriteLine("No purchase orders found.");
            return;
        }

        Console.WriteLine("\n========== Purchase Orders ==========");
        Console.WriteLine("{0,-10} {1,-20} {2,-15} {3,-12} {4,-15}", "PO ID", "Vendor", "Order Date", "Total Amount", "Status");
        Console.WriteLine(new string('-', 72));

        foreach (var order in purchaseOrders)
        {
            Console.WriteLine("{0,-10} {1,-20} {2,-15:yyyy-MM-dd} {3,-12:C} {4,-15}", order.PurchaseOrderId, order.VendorName, order.OrderDate, order.TotalAmount, order.Status);
        }
        Console.WriteLine();
    }

    public void DisplayDashboard()
    {
        decimal totalSales = salesOrders.Sum(o => o.TotalAmount);
        decimal totalPurchases = purchaseOrders.Sum(o => o.TotalAmount);
        decimal inventoryValue = products.Sum(p => p.UnitPrice * p.QuantityInStock);
        int lowStockProducts = products.Count(p => p.QuantityInStock <= p.ReorderLevel);

        Console.WriteLine("\n========== ERP Dashboard ==========");
        Console.WriteLine($"Total Employees: {employees.Count(e => e.IsActive)}");
        Console.WriteLine($"Total Customers: {customers.Count}");
        Console.WriteLine($"Total Products: {products.Count}");
        Console.WriteLine($"Low Stock Items: {lowStockProducts}");
        Console.WriteLine($"Total Sales: {totalSales:C}");
        Console.WriteLine($"Total Purchases: {totalPurchases:C}");
        Console.WriteLine($"Inventory Value: {inventoryValue:C}");
        Console.WriteLine($"Total Payroll: {employees.Where(e => e.IsActive).Sum(e => e.Salary):C}");
        Console.WriteLine();
    }
}

public class Program
{
    public static void Main()
    {
        ERPSystem erp = new ERPSystem();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n========== Mini ERP System ==========");
            Console.WriteLine("1. Employee Management");
            Console.WriteLine("2. Inventory Management");
            Console.WriteLine("3. Customer Management");
            Console.WriteLine("4. Sales Orders");
            Console.WriteLine("5. Purchase Orders");
            Console.WriteLine("6. Dashboard");
            Console.WriteLine("7. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ManageEmployees(erp);
                    break;
                case "2":
                    ManageInventory(erp);
                    break;
                case "3":
                    ManageCustomers(erp);
                    break;
                case "4":
                    ManageSalesOrders(erp);
                    break;
                case "5":
                    ManagePurchaseOrders(erp);
                    break;
                case "6":
                    erp.DisplayDashboard();
                    break;
                case "7":
                    running = false;
                    Console.WriteLine("Exiting ERP System...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private static void ManageEmployees(ERPSystem erp)
    {
        Console.WriteLine("\n========== Employee Management ==========");
        Console.WriteLine("1. Add Employee");
        Console.WriteLine("2. View Employees");
        Console.WriteLine("3. Back");
        Console.Write("Choose an option: ");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Write("Enter employee name: ");
                string name = Console.ReadLine();

                Console.Write("Enter position: ");
                string position = Console.ReadLine();

                Console.Write("Enter salary: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal salary))
                {
                    erp.AddEmployee(name, position, salary);
                }
                else
                {
                    Console.WriteLine("Invalid salary.");
                }
                break;
            case "2":
                erp.DisplayEmployees();
                break;
        }
    }

    private static void ManageInventory(ERPSystem erp)
    {
        Console.WriteLine("\n========== Inventory Management ==========");
        Console.WriteLine("1. Add Product");
        Console.WriteLine("2. View Inventory");
        Console.WriteLine("3. Back");
        Console.Write("Choose an option: ");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Write("Enter product name: ");
                string productName = Console.ReadLine();

                Console.Write("Enter category: ");
                string category = Console.ReadLine();

                Console.Write("Enter unit price: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal price))
                {
                    Console.WriteLine("Invalid price.");
                    return;
                }

                Console.Write("Enter quantity in stock: ");
                if (!int.TryParse(Console.ReadLine(), out int quantity))
                {
                    Console.WriteLine("Invalid quantity.");
                    return;
                }

                Console.Write("Enter reorder level: ");
                if (int.TryParse(Console.ReadLine(), out int reorderLevel))
                {
                    erp.AddProduct(productName, category, price, quantity, reorderLevel);
                }
                else
                {
                    Console.WriteLine("Invalid reorder level.");
                }
                break;
            case "2":
                erp.DisplayInventory();
                break;
        }
    }

    private static void ManageCustomers(ERPSystem erp)
    {
        Console.WriteLine("\n========== Customer Management ==========");
        Console.WriteLine("1. Add Customer");
        Console.WriteLine("2. View Customers");
        Console.WriteLine("3. Back");
        Console.Write("Choose an option: ");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Write("Enter customer name: ");
                string custName = Console.ReadLine();

                Console.Write("Enter email: ");
                string email = Console.ReadLine();

                Console.Write("Enter phone: ");
                string phone = Console.ReadLine();

                Console.Write("Enter address: ");
                string address = Console.ReadLine();

                erp.AddCustomer(custName, email, phone, address);
                break;
            case "2":
                erp.DisplayCustomers();
                break;
        }
    }

    private static void ManageSalesOrders(ERPSystem erp)
    {
        Console.WriteLine("\n========== Sales Orders ==========");
        Console.WriteLine("1. Create Sales Order");
        Console.WriteLine("2. View Sales Orders");
        Console.WriteLine("3. Back");
        Console.Write("Choose an option: ");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Write("Enter customer ID: ");
                if (int.TryParse(Console.ReadLine(), out int customerId))
                {
                    var items = new List<(int, int)>();
                    bool addingItems = true;

                    while (addingItems)
                    {
                        Console.Write("Enter product ID (0 to finish): ");
                        if (int.TryParse(Console.ReadLine(), out int productId) && productId != 0)
                        {
                            Console.Write("Enter quantity: ");
                            if (int.TryParse(Console.ReadLine(), out int itemQuantity))
                            {
                                items.Add((productId, itemQuantity));
                            }
                        }
                        else
                        {
                            addingItems = false;
                        }
                    }

                    if (items.Count > 0)
                    {
                        erp.CreateSalesOrder(customerId, items);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid customer ID.");
                }
                break;
            case "2":
                erp.DisplaySalesOrders();
                break;
        }
    }

    private static void ManagePurchaseOrders(ERPSystem erp)
    {
        Console.WriteLine("\n========== Purchase Orders ==========");
        Console.WriteLine("1. Create Purchase Order");
        Console.WriteLine("2. View Purchase Orders");
        Console.WriteLine("3. Back");
        Console.Write("Choose an option: ");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Write("Enter vendor name: ");
                string vendorName = Console.ReadLine();

                var poItems = new List<(int, int, decimal)>();
                bool addingPOItems = true;

                while (addingPOItems)
                {
                    Console.Write("Enter product ID (0 to finish): ");
                    if (int.TryParse(Console.ReadLine(), out int productId) && productId != 0)
                    {
                        Console.Write("Enter quantity: ");
                        if (int.TryParse(Console.ReadLine(), out int poQuantity))
                        {
                            Console.Write("Enter unit price: ");
                            if (decimal.TryParse(Console.ReadLine(), out decimal poPrice))
                            {
                                poItems.Add((productId, poQuantity, poPrice));
                            }
                        }
                    }
                    else
                    {
                        addingPOItems = false;
                    }
                }

                if (poItems.Count > 0)
                {
                    erp.CreatePurchaseOrder(vendorName, poItems);
                }
                break;
            case "2":
                erp.DisplayPurchaseOrders();
                break;
        }
    }
}
