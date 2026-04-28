using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Category { get; set; }
}

public class ProductManager
{
    private readonly string _filePath = "products.json";
    private List<Product> _products;

    public ProductManager()
    {
        LoadProducts();
    }

    private void LoadProducts()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            _products = JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
        }
        else
        {
            _products = new List<Product>();
        }
    }

    private void SaveProducts()
    {
        string json = JsonSerializer.Serialize(_products, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }

    public void Create(Product product)
    {
        product.Id = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;
        _products.Add(product);
        SaveProducts();
        Console.WriteLine($"Product '{product.Name}' created successfully with ID: {product.Id}");
    }

    public void Read()
    {
        if (_products.Count == 0)
        {
            Console.WriteLine("No products found.");
            return;
        }

        Console.WriteLine("\n========== All Products ==========");
        Console.WriteLine("{0,-5} {1,-20} {2,-10} {3,-10} {4,-15}", "ID", "Name", "Price", "Quantity", "Category");
        Console.WriteLine(new string('-', 60));

        foreach (var product in _products)
        {
            Console.WriteLine("{0,-5} {1,-20} {2,-10:C} {3,-10} {4,-15}", 
                product.Id, product.Name, product.Price, product.Quantity, product.Category);
        }
        Console.WriteLine();
    }

    public void Update(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            Console.WriteLine($"Product with ID {id} not found.");
            return;
        }

        Console.Write("Enter new name (current: {0}): ", product.Name);
        string name = Console.ReadLine();
        if (!string.IsNullOrEmpty(name)) product.Name = name;

        Console.Write("Enter new price (current: {0}): ", product.Price);
        if (decimal.TryParse(Console.ReadLine(), out decimal price)) product.Price = price;

        Console.Write("Enter new quantity (current: {0}): ", product.Quantity);
        if (int.TryParse(Console.ReadLine(), out int quantity)) product.Quantity = quantity;

        Console.Write("Enter new category (current: {0}): ", product.Category);
        string category = Console.ReadLine();
        if (!string.IsNullOrEmpty(category)) product.Category = category;

        SaveProducts();
        Console.WriteLine("Product updated successfully.");
    }

    public void Delete(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            Console.WriteLine($"Product with ID {id} not found.");
            return;
        }

        _products.Remove(product);
        SaveProducts();
        Console.WriteLine($"Product '{product.Name}' deleted successfully.");
    }

    public void Search(string name)
    {
        var results = _products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        
        if (results.Count == 0)
        {
            Console.WriteLine($"No products found with name containing '{name}'.");
            return;
        }

        Console.WriteLine("\n========== Search Results ==========");
        Console.WriteLine("{0,-5} {1,-20} {2,-10} {3,-10} {4,-15}", "ID", "Name", "Price", "Quantity", "Category");
        Console.WriteLine(new string('-', 60));

        foreach (var product in results)
        {
            Console.WriteLine("{0,-5} {1,-20} {2,-10:C} {3,-10} {4,-15}", 
                product.Id, product.Name, product.Price, product.Quantity, product.Category);
        }
        Console.WriteLine();
    }
}

public class Program
{
    public static void Main()
    {
        ProductManager manager = new ProductManager();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n========== Product Management System ==========");
            Console.WriteLine("1. Create Product");
            Console.WriteLine("2. View All Products");
            Console.WriteLine("3. Update Product");
            Console.WriteLine("4. Delete Product");
            Console.WriteLine("5. Search Product");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateProduct(manager);
                    break;
                case "2":
                    manager.Read();
                    break;
                case "3":
                    UpdateProduct(manager);
                    break;
                case "4":
                    DeleteProduct(manager);
                    break;
                case "5":
                    SearchProduct(manager);
                    break;
                case "6":
                    running = false;
                    Console.WriteLine("Exiting application...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private static void CreateProduct(ProductManager manager)
    {
        Console.Write("Enter product name: ");
        string name = Console.ReadLine();

        Console.Write("Enter product price: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal price))
        {
            Console.WriteLine("Invalid price.");
            return;
        }

        Console.Write("Enter product quantity: ");
        if (!int.TryParse(Console.ReadLine(), out int quantity))
        {
            Console.WriteLine("Invalid quantity.");
            return;
        }

        Console.Write("Enter product category: ");
        string category = Console.ReadLine();

        Product product = new Product
        {
            Name = name,
            Price = price,
            Quantity = quantity,
            Category = category
        };

        manager.Create(product);
    }

    private static void UpdateProduct(ProductManager manager)
    {
        Console.Write("Enter product ID to update: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            manager.Update(id);
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }
    }

    private static void DeleteProduct(ProductManager manager)
    {
        Console.Write("Enter product ID to delete: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            manager.Delete(id);
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }
    }

    private static void SearchProduct(ProductManager manager)
    {
        Console.Write("Enter product name to search: ");
        string name = Console.ReadLine();
        manager.Search(name);
    }
}
