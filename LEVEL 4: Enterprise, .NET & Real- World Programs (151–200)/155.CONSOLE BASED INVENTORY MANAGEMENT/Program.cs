using System;
using System.Collections.Generic;
using System.Linq;

enum ProductStatus
{
    Active,
    Discontinued,
    OutOfStock
}

enum MovementType
{
    Inbound,
    Outbound,
    Adjustment,
    Return,
    Damage
}

enum AlertType
{
    LowStock,
    Expired,
    Overstocked,
    None
}

class Category
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }

    public Category(int categoryId, string name, string description)
    {
        CategoryId = categoryId;
        CategoryName = name;
        Description = description;
    }

    public override string ToString()
    {
        return $"ID: {CategoryId} | Category: {CategoryName}";
    }
}

class Supplier
{
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public decimal LeadTimeDays { get; set; }

    public Supplier(int supplierId, string name, string email, string phone, string address, decimal leadDays)
    {
        SupplierId = supplierId;
        SupplierName = name;
        Email = email;
        PhoneNumber = phone;
        Address = address;
        LeadTimeDays = leadDays;
    }

    public override string ToString()
    {
        return $"ID: {SupplierId} | {SupplierName} | Email: {Email} | Lead Time: {LeadTimeDays} days";
    }
}

class InventoryMovement
{
    public int MovementId { get; set; }
    public string SKU { get; set; }
    public MovementType Type { get; set; }
    public int Quantity { get; set; }
    public DateTime MovementDate { get; set; }
    public string Reason { get; set; }
    public decimal UnitCost { get; set; }

    public InventoryMovement(int movementId, string sku, MovementType type, int quantity, string reason, decimal unitCost)
    {
        MovementId = movementId;
        SKU = sku;
        Type = type;
        Quantity = quantity;
        MovementDate = DateTime.Now;
        Reason = reason;
        UnitCost = unitCost;
    }

    public override string ToString()
    {
        return $"ID: {MovementId} | SKU: {SKU} | Type: {Type} | Qty: {Quantity} | Date: {MovementDate:yyyy-MM-dd HH:mm:ss} | {Reason}";
    }
}

class Product
{
    public string SKU { get; set; }
    public string ProductName { get; set; }
    public int CategoryId { get; set; }
    public int SupplierId { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal UnitCost { get; set; }
    public int CurrentStock { get; set; }
    public int MinimumStock { get; set; }
    public int MaximumStock { get; set; }
    public int ReorderQuantity { get; set; }
    public DateTime ExpiryDate { get; set; }
    public ProductStatus Status { get; set; }
    public List<InventoryMovement> Movements { get; set; }
    public DateTime CreatedDate { get; set; }

    public Product(string sku, string name, int categoryId, int supplierId, decimal unitPrice, decimal unitCost, 
                   int minStock, int maxStock, int reorderQty, DateTime expiry)
    {
        SKU = sku;
        ProductName = name;
        CategoryId = categoryId;
        SupplierId = supplierId;
        UnitPrice = unitPrice;
        UnitCost = unitCost;
        CurrentStock = 0;
        MinimumStock = minStock;
        MaximumStock = maxStock;
        ReorderQuantity = reorderQty;
        ExpiryDate = expiry;
        Status = ProductStatus.Active;
        Movements = new List<InventoryMovement>();
        CreatedDate = DateTime.Now;
    }

    public AlertType GetAlert()
    {
        if (DateTime.Now > ExpiryDate)
        {
            return AlertType.Expired;
        }
        if (CurrentStock <= MinimumStock)
        {
            return AlertType.LowStock;
        }
        if (CurrentStock >= MaximumStock)
        {
            return AlertType.Overstocked;
        }
        return AlertType.None;
    }

    public decimal GetInventoryValue()
    {
        return CurrentStock * UnitCost;
    }

    public override string ToString()
    {
        return $"SKU: {SKU} | Product: {ProductName} | Price: ${UnitPrice:F2} | Stock: {CurrentStock} | Status: {Status}";
    }
}

class InventoryManagementSystem
{
    private List<Product> products = new List<Product>();
    private List<Category> categories = new List<Category>();
    private List<Supplier> suppliers = new List<Supplier>();
    private List<InventoryMovement> allMovements = new List<InventoryMovement>();
    private int nextMovementId = 1;

    public void AddCategory(int categoryId, string name, string description)
    {
        if (categories.Any(c => c.CategoryId == categoryId))
        {
            Console.WriteLine("Category ID already exists!");
            return;
        }

        categories.Add(new Category(categoryId, name, description));
        Console.WriteLine("Category added successfully!");
    }

    public void AddSupplier(int supplierId, string name, string email, string phone, string address, decimal leadDays)
    {
        if (suppliers.Any(s => s.SupplierId == supplierId))
        {
            Console.WriteLine("Supplier ID already exists!");
            return;
        }

        suppliers.Add(new Supplier(supplierId, name, email, phone, address, leadDays));
        Console.WriteLine("Supplier added successfully!");
    }

    public void AddProduct(string sku, string name, int categoryId, int supplierId, decimal unitPrice, decimal unitCost, 
                           int minStock, int maxStock, int reorderQty, DateTime expiry, int initialStock)
    {
        if (products.Any(p => p.SKU == sku))
        {
            Console.WriteLine("Product with this SKU already exists!");
            return;
        }

        if (!categories.Any(c => c.CategoryId == categoryId))
        {
            Console.WriteLine("Category not found!");
            return;
        }

        if (!suppliers.Any(s => s.SupplierId == supplierId))
        {
            Console.WriteLine("Supplier not found!");
            return;
        }

        var product = new Product(sku, name, categoryId, supplierId, unitPrice, unitCost, minStock, maxStock, reorderQty, expiry);
        products.Add(product);

        if (initialStock > 0)
        {
            AddInventoryMovement(sku, MovementType.Inbound, initialStock, "Initial Stock", unitCost);
        }

        Console.WriteLine($"Product added successfully! SKU: {sku}");
    }

    public void AddInventoryMovement(string sku, MovementType type, int quantity, string reason, decimal unitCost)
    {
        var product = products.FirstOrDefault(p => p.SKU == sku);
        if (product == null)
        {
            Console.WriteLine("Product not found!");
            return;
        }

        if (quantity <= 0)
        {
            Console.WriteLine("Quantity must be greater than 0!");
            return;
        }

        if (type == MovementType.Outbound || type == MovementType.Return)
        {
            if (product.CurrentStock < quantity)
            {
                Console.WriteLine($"Insufficient stock! Available: {product.CurrentStock}");
                return;
            }
        }

        if (type == MovementType.Inbound || type == MovementType.Return)
        {
            product.CurrentStock += quantity;
        }
        else if (type == MovementType.Outbound || type == MovementType.Damage)
        {
            product.CurrentStock -= quantity;
        }

        if (product.CurrentStock <= 0)
        {
            product.Status = ProductStatus.OutOfStock;
        }
        else if (product.Status == ProductStatus.OutOfStock)
        {
            product.Status = ProductStatus.Active;
        }

        var movement = new InventoryMovement(nextMovementId++, sku, type, quantity, reason, unitCost);
        product.Movements.Add(movement);
        allMovements.Add(movement);

        Console.WriteLine($"Inventory movement recorded! New Stock: {product.CurrentStock}");
    }

    public void UpdateProductPrice(string sku, decimal newPrice)
    {
        var product = products.FirstOrDefault(p => p.SKU == sku);
        if (product == null)
        {
            Console.WriteLine("Product not found!");
            return;
        }

        decimal oldPrice = product.UnitPrice;
        product.UnitPrice = newPrice;
        Console.WriteLine($"Price updated! Old: ${oldPrice:F2}, New: ${newPrice:F2}");
    }

    public void UpdateProductCost(string sku, decimal newCost)
    {
        var product = products.FirstOrDefault(p => p.SKU == sku);
        if (product == null)
        {
            Console.WriteLine("Product not found!");
            return;
        }

        decimal oldCost = product.UnitCost;
        product.UnitCost = newCost;
        Console.WriteLine($"Cost updated! Old: ${oldCost:F2}, New: ${newCost:F2}");
    }

    public void UpdateReorderLevel(string sku, int minStock, int maxStock, int reorderQty)
    {
        var product = products.FirstOrDefault(p => p.SKU == sku);
        if (product == null)
        {
            Console.WriteLine("Product not found!");
            return;
        }

        product.MinimumStock = minStock;
        product.MaximumStock = maxStock;
        product.ReorderQuantity = reorderQty;
        Console.WriteLine("Reorder levels updated!");
    }

    public void DiscontinueProduct(string sku)
    {
        var product = products.FirstOrDefault(p => p.SKU == sku);
        if (product == null)
        {
            Console.WriteLine("Product not found!");
            return;
        }

        product.Status = ProductStatus.Discontinued;
        Console.WriteLine("Product discontinued!");
    }

    public void CheckProductAlerts(string sku)
    {
        var product = products.FirstOrDefault(p => p.SKU == sku);
        if (product == null)
        {
            Console.WriteLine("Product not found!");
            return;
        }

        AlertType alert = product.GetAlert();
        if (alert == AlertType.None)
        {
            Console.WriteLine("No alerts for this product.");
            return;
        }

        Console.WriteLine($"ALERT for {product.ProductName}: {alert}");
    }

    public void ViewLowStockItems()
    {
        var lowStockItems = products.Where(p => p.CurrentStock <= p.MinimumStock && p.Status == ProductStatus.Active).ToList();

        if (lowStockItems.Count == 0)
        {
            Console.WriteLine("No low stock items!");
            return;
        }

        Console.WriteLine("\n========== Low Stock Items ==========");
        foreach (var product in lowStockItems)
        {
            var supplier = suppliers.FirstOrDefault(s => s.SupplierId == product.SupplierId);
            Console.WriteLine($"SKU: {product.SKU} | {product.ProductName} | Stock: {product.CurrentStock} | Reorder: {product.ReorderQuantity} | Supplier: {supplier?.SupplierName}");
        }
        Console.WriteLine("=====================================\n");
    }

    public void ViewExpiredProducts()
    {
        var expiredProducts = products.Where(p => DateTime.Now > p.ExpiryDate).ToList();

        if (expiredProducts.Count == 0)
        {
            Console.WriteLine("No expired products!");
            return;
        }

        Console.WriteLine("\n========== Expired Products ==========");
        foreach (var product in expiredProducts)
        {
            Console.WriteLine($"SKU: {product.SKU} | {product.ProductName} | Expired: {product.ExpiryDate:yyyy-MM-dd}");
        }
        Console.WriteLine("======================================\n");
    }

    public void ViewProductDetails(string sku)
    {
        var product = products.FirstOrDefault(p => p.SKU == sku);
        if (product == null)
        {
            Console.WriteLine("Product not found!");
            return;
        }

        var category = categories.FirstOrDefault(c => c.CategoryId == product.CategoryId);
        var supplier = suppliers.FirstOrDefault(s => s.SupplierId == product.SupplierId);

        Console.WriteLine("\n========== Product Details ==========");
        Console.WriteLine($"SKU: {product.SKU}");
        Console.WriteLine($"Product: {product.ProductName}");
        Console.WriteLine($"Category: {category?.CategoryName}");
        Console.WriteLine($"Supplier: {supplier?.SupplierName}");
        Console.WriteLine($"Unit Price: ${product.UnitPrice:F2}");
        Console.WriteLine($"Unit Cost: ${product.UnitCost:F2}");
        Console.WriteLine($"Current Stock: {product.CurrentStock}");
        Console.WriteLine($"Min Stock: {product.MinimumStock}, Max Stock: {product.MaximumStock}");
        Console.WriteLine($"Reorder Quantity: {product.ReorderQuantity}");
        Console.WriteLine($"Expiry Date: {product.ExpiryDate:yyyy-MM-dd}");
        Console.WriteLine($"Status: {product.Status}");
        Console.WriteLine($"Alert: {product.GetAlert()}");
        Console.WriteLine($"Inventory Value: ${product.GetInventoryValue():F2}");
        Console.WriteLine("====================================\n");
    }

    public void ViewProductMovements(string sku)
    {
        var product = products.FirstOrDefault(p => p.SKU == sku);
        if (product == null)
        {
            Console.WriteLine("Product not found!");
            return;
        }

        if (product.Movements.Count == 0)
        {
            Console.WriteLine("No movements for this product!");
            return;
        }

        Console.WriteLine($"\n========== Movements for {product.ProductName} ==========");
        foreach (var movement in product.Movements.OrderByDescending(m => m.MovementDate))
        {
            Console.WriteLine(movement);
        }
        Console.WriteLine("========================================================\n");
    }

    public void SearchProducts(string searchTerm)
    {
        var results = products.Where(p =>
            p.SKU.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
            p.ProductName.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0
        ).ToList();

        if (results.Count == 0)
        {
            Console.WriteLine("No products found!");
            return;
        }

        Console.WriteLine("\n========== Search Results ==========");
        foreach (var product in results)
        {
            Console.WriteLine(product);
        }
        Console.WriteLine("====================================\n");
    }

    public void ViewCategoryInventory(int categoryId)
    {
        var categoryProducts = products.Where(p => p.CategoryId == categoryId).ToList();

        if (categoryProducts.Count == 0)
        {
            Console.WriteLine("No products in this category!");
            return;
        }

        var category = categories.FirstOrDefault(c => c.CategoryId == categoryId);
        Console.WriteLine($"\n========== {category?.CategoryName} Inventory ==========");
        foreach (var product in categoryProducts)
        {
            Console.WriteLine(product);
        }
        Console.WriteLine("================================================\n");
    }

    public void ViewAllProducts()
    {
        if (products.Count == 0)
        {
            Console.WriteLine("No products in inventory!");
            return;
        }

        Console.WriteLine("\n========== All Products ==========");
        foreach (var product in products)
        {
            Console.WriteLine(product);
        }
        Console.WriteLine("==================================\n");
    }

    public void ViewInventorySummary()
    {
        if (products.Count == 0)
        {
            Console.WriteLine("No products in inventory!");
            return;
        }

        Console.WriteLine("\n========== Inventory Summary ==========");
        Console.WriteLine($"Total Products: {products.Count}");
        Console.WriteLine($"Active Products: {products.Count(p => p.Status == ProductStatus.Active)}");
        Console.WriteLine($"Out of Stock: {products.Count(p => p.Status == ProductStatus.OutOfStock)}");
        Console.WriteLine($"Discontinued: {products.Count(p => p.Status == ProductStatus.Discontinued)}");
        Console.WriteLine($"\nTotal Inventory Value: ${products.Sum(p => p.GetInventoryValue()):F2}");
        Console.WriteLine($"Total Units in Stock: {products.Sum(p => p.CurrentStock)}");
        Console.WriteLine($"Total Movements: {allMovements.Count}");

        var lowStock = products.Count(p => p.CurrentStock <= p.MinimumStock);
        var expired = products.Count(p => DateTime.Now > p.ExpiryDate);
        Console.WriteLine($"\nLow Stock Items: {lowStock}");
        Console.WriteLine($"Expired Items: {expired}");
        Console.WriteLine("=======================================\n");
    }

    public void GenerateInventoryReport()
    {
        Console.WriteLine("\n========== Detailed Inventory Report ==========");
        
        var byCategory = products.GroupBy(p => p.CategoryId);
        foreach (var group in byCategory)
        {
            var category = categories.FirstOrDefault(c => c.CategoryId == group.Key);
            Console.WriteLine($"\n{category?.CategoryName}:");
            decimal categoryValue = 0;
            int categoryUnits = 0;
            foreach (var product in group)
            {
                Console.WriteLine($"  {product.SKU}: {product.ProductName} | Stock: {product.CurrentStock} | Value: ${product.GetInventoryValue():F2}");
                categoryValue += product.GetInventoryValue();
                categoryUnits += product.CurrentStock;
            }
            Console.WriteLine($"  Category Total: {categoryUnits} units, ${categoryValue:F2}");
        }
        Console.WriteLine("\n==============================================\n");
    }

    public void GenerateMovementReport(DateTime startDate, DateTime endDate)
    {
        var filteredMovements = allMovements.Where(m => m.MovementDate >= startDate && m.MovementDate <= endDate).ToList();

        if (filteredMovements.Count == 0)
        {
            Console.WriteLine("No movements found in this date range!");
            return;
        }

        Console.WriteLine($"\n========== Movement Report ({startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}) ==========");
        foreach (var movement in filteredMovements.OrderByDescending(m => m.MovementDate))
        {
            Console.WriteLine(movement);
        }
        Console.WriteLine("==================================================================\n");
    }
}

class Program
{
    static void Main()
    {
        InventoryManagementSystem inventory = new InventoryManagementSystem();

        inventory.AddCategory(1, "Electronics", "Electronic devices");
        inventory.AddCategory(2, "Clothing", "Apparel items");
        inventory.AddCategory(3, "Books", "Educational materials");
        inventory.AddCategory(4, "Food", "Food products");

        inventory.AddSupplier(101, "Global Supplies Co", "contact@globalsupply.com", "555-0101", "123 Supply St", 7);
        inventory.AddSupplier(102, "Quality Imports", "info@qualityimports.com", "555-0102", "456 Import Ave", 14);
        inventory.AddSupplier(103, "Local Vendors", "sales@localvendors.com", "555-0103", "789 Vendor Rd", 3);

        inventory.AddProduct("LAPTOP001", "Laptop Pro 15", 1, 101, 1299.99m, 850m, 5, 20, 10, new DateTime(2027, 12, 31), 12);
        inventory.AddProduct("SHIRT001", "Cotton T-Shirt", 2, 102, 19.99m, 8m, 20, 100, 50, new DateTime(2026, 12, 31), 65);
        inventory.AddProduct("BOOK001", "C# Programming", 3, 103, 49.99m, 25m, 10, 50, 25, new DateTime(2028, 6, 30), 35);
        inventory.AddProduct("FOOD001", "Organic Coffee", 4, 103, 9.99m, 4m, 30, 150, 75, new DateTime(2026, 8, 15), 95);
        inventory.AddProduct("PHONE001", "Smartphone X", 1, 101, 899.99m, 500m, 8, 30, 15, new DateTime(2027, 9, 30), 18);

        bool running = true;
        while (running)
        {
            Console.WriteLine("\n================ Inventory Management System ================");
            Console.WriteLine("1. Add Category");
            Console.WriteLine("2. Add Supplier");
            Console.WriteLine("3. Add Product");
            Console.WriteLine("4. Record Inventory Movement");
            Console.WriteLine("5. Update Product Price");
            Console.WriteLine("6. Update Product Cost");
            Console.WriteLine("7. Update Reorder Levels");
            Console.WriteLine("8. Discontinue Product");
            Console.WriteLine("9. Check Product Alerts");
            Console.WriteLine("10. View Low Stock Items");
            Console.WriteLine("11. View Expired Products");
            Console.WriteLine("12. View Product Details");
            Console.WriteLine("13. View Product Movements");
            Console.WriteLine("14. Search Products");
            Console.WriteLine("15. View Category Inventory");
            Console.WriteLine("16. View All Products");
            Console.WriteLine("17. View Inventory Summary");
            Console.WriteLine("18. Generate Inventory Report");
            Console.WriteLine("19. Generate Movement Report");
            Console.WriteLine("20. Exit");
            Console.WriteLine("===========================================================");
            Console.Write("Enter your choice (1-20): ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        Console.Write("Enter Category ID: ");
                        if (int.TryParse(Console.ReadLine(), out int catId))
                        {
                            Console.Write("Enter Category Name: ");
                            string catName = Console.ReadLine();
                            Console.Write("Enter Description: ");
                            string catDesc = Console.ReadLine();
                            inventory.AddCategory(catId, catName, catDesc);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Category ID!");
                        }
                        break;

                    case 2:
                        Console.Write("Enter Supplier ID: ");
                        if (int.TryParse(Console.ReadLine(), out int supId))
                        {
                            Console.Write("Enter Supplier Name: ");
                            string supName = Console.ReadLine();
                            Console.Write("Enter Email: ");
                            string supEmail = Console.ReadLine();
                            Console.Write("Enter Phone: ");
                            string supPhone = Console.ReadLine();
                            Console.Write("Enter Address: ");
                            string supAddr = Console.ReadLine();
                            Console.Write("Enter Lead Time (days): ");
                            if (decimal.TryParse(Console.ReadLine(), out decimal leadTime) && leadTime > 0)
                            {
                                inventory.AddSupplier(supId, supName, supEmail, supPhone, supAddr, leadTime);
                            }
                            else
                            {
                                Console.WriteLine("Invalid lead time!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Supplier ID!");
                        }
                        break;

                    case 3:
                        Console.Write("Enter SKU: ");
                        string sku = Console.ReadLine();
                        Console.Write("Enter Product Name: ");
                        string prodName = Console.ReadLine();
                        Console.Write("Enter Category ID: ");
                        if (int.TryParse(Console.ReadLine(), out int prodCatId))
                        {
                            Console.Write("Enter Supplier ID: ");
                            if (int.TryParse(Console.ReadLine(), out int prodSupId))
                            {
                                Console.Write("Enter Unit Price: ");
                                if (decimal.TryParse(Console.ReadLine(), out decimal unitPrice) && unitPrice > 0)
                                {
                                    Console.Write("Enter Unit Cost: ");
                                    if (decimal.TryParse(Console.ReadLine(), out decimal unitCost) && unitCost > 0)
                                    {
                                        Console.Write("Enter Minimum Stock: ");
                                        if (int.TryParse(Console.ReadLine(), out int minStock) && minStock >= 0)
                                        {
                                            Console.Write("Enter Maximum Stock: ");
                                            if (int.TryParse(Console.ReadLine(), out int maxStock) && maxStock > minStock)
                                            {
                                                Console.Write("Enter Reorder Quantity: ");
                                                if (int.TryParse(Console.ReadLine(), out int reorderQty) && reorderQty > 0)
                                                {
                                                    Console.Write("Enter Expiry Date (yyyy-MM-dd): ");
                                                    if (DateTime.TryParse(Console.ReadLine(), out DateTime expiry))
                                                    {
                                                        Console.Write("Enter Initial Stock: ");
                                                        if (int.TryParse(Console.ReadLine(), out int initStock) && initStock >= 0)
                                                        {
                                                            inventory.AddProduct(sku, prodName, prodCatId, prodSupId, unitPrice, unitCost, minStock, maxStock, reorderQty, expiry, initStock);
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Invalid initial stock!");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Invalid date format!");
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Invalid reorder quantity!");
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Invalid maximum stock!");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid minimum stock!");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid unit cost!");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid unit price!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid Supplier ID!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Category ID!");
                        }
                        break;

                    case 4:
                        Console.Write("Enter SKU: ");
                        string moveSku = Console.ReadLine();
                        Console.WriteLine("Movement Types: 0=Inbound, 1=Outbound, 2=Adjustment, 3=Return, 4=Damage");
                        Console.Write("Enter Movement Type (0-4): ");
                        if (int.TryParse(Console.ReadLine(), out int moveType) && moveType >= 0 && moveType <= 4)
                        {
                            Console.Write("Enter Quantity: ");
                            if (int.TryParse(Console.ReadLine(), out int moveQty) && moveQty > 0)
                            {
                                Console.Write("Enter Reason: ");
                                string moveReason = Console.ReadLine();
                                Console.Write("Enter Unit Cost: ");
                                if (decimal.TryParse(Console.ReadLine(), out decimal moveCost) && moveCost >= 0)
                                {
                                    inventory.AddInventoryMovement(moveSku, (MovementType)moveType, moveQty, moveReason, moveCost);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid unit cost!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid quantity!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid movement type!");
                        }
                        break;

                    case 5:
                        Console.Write("Enter SKU: ");
                        string priceSkU = Console.ReadLine();
                        Console.Write("Enter New Price: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal newPrice) && newPrice > 0)
                        {
                            inventory.UpdateProductPrice(priceSkU, newPrice);
                        }
                        else
                        {
                            Console.WriteLine("Invalid price!");
                        }
                        break;

                    case 6:
                        Console.Write("Enter SKU: ");
                        string costSku = Console.ReadLine();
                        Console.Write("Enter New Cost: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal newCost) && newCost > 0)
                        {
                            inventory.UpdateProductCost(costSku, newCost);
                        }
                        else
                        {
                            Console.WriteLine("Invalid cost!");
                        }
                        break;

                    case 7:
                        Console.Write("Enter SKU: ");
                        string reorderSku = Console.ReadLine();
                        Console.Write("Enter Minimum Stock: ");
                        if (int.TryParse(Console.ReadLine(), out int newMin) && newMin >= 0)
                        {
                            Console.Write("Enter Maximum Stock: ");
                            if (int.TryParse(Console.ReadLine(), out int newMax) && newMax > newMin)
                            {
                                Console.Write("Enter Reorder Quantity: ");
                                if (int.TryParse(Console.ReadLine(), out int newReorder) && newReorder > 0)
                                {
                                    inventory.UpdateReorderLevel(reorderSku, newMin, newMax, newReorder);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid reorder quantity!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid maximum stock!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid minimum stock!");
                        }
                        break;

                    case 8:
                        Console.Write("Enter SKU: ");
                        string discSku = Console.ReadLine();
                        inventory.DiscontinueProduct(discSku);
                        break;

                    case 9:
                        Console.Write("Enter SKU: ");
                        string alertSku = Console.ReadLine();
                        inventory.CheckProductAlerts(alertSku);
                        break;

                    case 10:
                        inventory.ViewLowStockItems();
                        break;

                    case 11:
                        inventory.ViewExpiredProducts();
                        break;

                    case 12:
                        Console.Write("Enter SKU: ");
                        string detailSku = Console.ReadLine();
                        inventory.ViewProductDetails(detailSku);
                        break;

                    case 13:
                        Console.Write("Enter SKU: ");
                        string movementSku = Console.ReadLine();
                        inventory.ViewProductMovements(movementSku);
                        break;

                    case 14:
                        Console.Write("Enter Search Term: ");
                        string searchTerm = Console.ReadLine();
                        inventory.SearchProducts(searchTerm);
                        break;

                    case 15:
                        Console.Write("Enter Category ID: ");
                        if (int.TryParse(Console.ReadLine(), out int viewCatId))
                        {
                            inventory.ViewCategoryInventory(viewCatId);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Category ID!");
                        }
                        break;

                    case 16:
                        inventory.ViewAllProducts();
                        break;

                    case 17:
                        inventory.ViewInventorySummary();
                        break;

                    case 18:
                        inventory.GenerateInventoryReport();
                        break;

                    case 19:
                        Console.Write("Enter Start Date (yyyy-MM-dd): ");
                        if (DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
                        {
                            Console.Write("Enter End Date (yyyy-MM-dd): ");
                            if (DateTime.TryParse(Console.ReadLine(), out DateTime endDate) && endDate >= startDate)
                            {
                                inventory.GenerateMovementReport(startDate, endDate);
                            }
                            else
                            {
                                Console.WriteLine("Invalid date range!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid start date!");
                        }
                        break;

                    case 20:
                        running = false;
                        Console.WriteLine("Thank you for using Inventory Management System!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice! Please enter a number between 1 and 20.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input! Please enter a valid number.");
            }
        }
    }
}
