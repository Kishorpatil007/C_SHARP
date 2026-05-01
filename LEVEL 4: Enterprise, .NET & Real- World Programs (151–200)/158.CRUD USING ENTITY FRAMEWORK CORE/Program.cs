using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class Employee
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Department { get; set; }
    public decimal Salary { get; set; }
    public DateTime HireDate { get; set; }
}

public class EmployeeDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.;Database=EmployeeDB;Integrated Security=true;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee>()
            .HasKey(e => e.EmployeeId);

        modelBuilder.Entity<Employee>()
            .Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Employee>()
            .Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Employee>()
            .Property(e => e.Email)
            .HasMaxLength(150);

        modelBuilder.Entity<Employee>()
            .Property(e => e.Department)
            .HasMaxLength(100);

        modelBuilder.Entity<Employee>()
            .Property(e => e.Salary)
            .HasColumnType("decimal(10,2)");
    }
}

public class EmployeeManager
{
    private readonly EmployeeDbContext _context;

    public EmployeeManager()
    {
        _context = new EmployeeDbContext();
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        try
        {
            _context.Database.EnsureCreated();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database initialization error: {ex.Message}");
        }
    }

    public void Create(Employee employee)
    {
        try
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            Console.WriteLine("Employee created successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public List<Employee> ReadAll()
    {
        try
        {
            return _context.Employees.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return new List<Employee>();
        }
    }

    public Employee ReadById(int employeeId)
    {
        try
        {
            return _context.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }
    }

    public void Update(Employee employee)
    {
        try
        {
            var existingEmployee = _context.Employees.FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);
            if (existingEmployee != null)
            {
                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.Email = employee.Email;
                existingEmployee.Phone = employee.Phone;
                existingEmployee.Department = employee.Department;
                existingEmployee.Salary = employee.Salary;
                existingEmployee.HireDate = employee.HireDate;

                _context.SaveChanges();
                Console.WriteLine("Employee updated successfully.");
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public void Delete(int employeeId)
    {
        try
        {
            var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
                Console.WriteLine("Employee deleted successfully.");
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public List<Employee> GetByDepartment(string department)
    {
        try
        {
            return _context.Employees.Where(e => e.Department == department).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return new List<Employee>();
        }
    }

    public decimal GetAverageSalary()
    {
        try
        {
            return _context.Employees.Average(e => e.Salary);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return 0;
        }
    }

    public List<Employee> GetByNameContains(string name)
    {
        try
        {
            return _context.Employees
                .Where(e => e.FirstName.Contains(name) || e.LastName.Contains(name))
                .ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return new List<Employee>();
        }
    }

    public void DisplayAllEmployees()
    {
        List<Employee> employees = ReadAll();

        if (employees.Count == 0)
        {
            Console.WriteLine("No employees found.");
            return;
        }

        Console.WriteLine("\n========== All Employees ==========");
        Console.WriteLine("{0,-5} {1,-15} {2,-15} {3,-25} {4,-15} {5,-15} {6,-12} {7,-12}", 
            "ID", "First Name", "Last Name", "Email", "Phone", "Department", "Salary", "Hire Date");
        Console.WriteLine(new string('-', 114));

        foreach (var employee in employees)
        {
            Console.WriteLine("{0,-5} {1,-15} {2,-15} {3,-25} {4,-15} {5,-15} {6,-12:C} {7,-12:yyyy-MM-dd}", 
                employee.EmployeeId, employee.FirstName, employee.LastName, employee.Email, 
                employee.Phone, employee.Department, employee.Salary, employee.HireDate);
        }
        Console.WriteLine();
    }

    public void DisplayEmployee(Employee employee)
    {
        if (employee == null)
        {
            Console.WriteLine("Employee not found.");
            return;
        }

        Console.WriteLine("\n========== Employee Details ==========");
        Console.WriteLine($"ID: {employee.EmployeeId}");
        Console.WriteLine($"Name: {employee.FirstName} {employee.LastName}");
        Console.WriteLine($"Email: {employee.Email}");
        Console.WriteLine($"Phone: {employee.Phone}");
        Console.WriteLine($"Department: {employee.Department}");
        Console.WriteLine($"Salary: {employee.Salary:C}");
        Console.WriteLine($"Hire Date: {employee.HireDate:yyyy-MM-dd}");
        Console.WriteLine();
    }
}

public class Program
{
    public static void Main()
    {
        EmployeeManager manager = new EmployeeManager();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n========== Employee Management System (EF Core) ==========");
            Console.WriteLine("1. Create Employee");
            Console.WriteLine("2. View All Employees");
            Console.WriteLine("3. View Employee by ID");
            Console.WriteLine("4. Update Employee");
            Console.WriteLine("5. Delete Employee");
            Console.WriteLine("6. Search by Name");
            Console.WriteLine("7. View by Department");
            Console.WriteLine("8. Average Salary");
            Console.WriteLine("9. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateEmployee(manager);
                    break;
                case "2":
                    manager.DisplayAllEmployees();
                    break;
                case "3":
                    ViewEmployeeById(manager);
                    break;
                case "4":
                    UpdateEmployee(manager);
                    break;
                case "5":
                    DeleteEmployee(manager);
                    break;
                case "6":
                    SearchByName(manager);
                    break;
                case "7":
                    ViewByDepartment(manager);
                    break;
                case "8":
                    Console.WriteLine($"Average Salary: {manager.GetAverageSalary():C}");
                    break;
                case "9":
                    running = false;
                    Console.WriteLine("Exiting application...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private static void CreateEmployee(EmployeeManager manager)
    {
        Console.Write("Enter first name: ");
        string firstName = Console.ReadLine();

        Console.Write("Enter last name: ");
        string lastName = Console.ReadLine();

        Console.Write("Enter email: ");
        string email = Console.ReadLine();

        Console.Write("Enter phone: ");
        string phone = Console.ReadLine();

        Console.Write("Enter department: ");
        string department = Console.ReadLine();

        Console.Write("Enter salary: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal salary))
        {
            Console.WriteLine("Invalid salary.");
            return;
        }

        Console.Write("Enter hire date (yyyy-MM-dd): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime hireDate))
        {
            hireDate = DateTime.Now;
        }

        Employee employee = new Employee
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Phone = phone,
            Department = department,
            Salary = salary,
            HireDate = hireDate
        };

        manager.Create(employee);
    }

    private static void ViewEmployeeById(EmployeeManager manager)
    {
        Console.Write("Enter employee ID: ");
        if (int.TryParse(Console.ReadLine(), out int employeeId))
        {
            Employee employee = manager.ReadById(employeeId);
            manager.DisplayEmployee(employee);
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }
    }

    private static void UpdateEmployee(EmployeeManager manager)
    {
        Console.Write("Enter employee ID to update: ");
        if (!int.TryParse(Console.ReadLine(), out int employeeId))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        Employee employee = manager.ReadById(employeeId);
        if (employee == null)
        {
            Console.WriteLine("Employee not found.");
            return;
        }

        Console.Write($"Enter first name (current: {employee.FirstName}): ");
        string firstName = Console.ReadLine();
        if (!string.IsNullOrEmpty(firstName)) employee.FirstName = firstName;

        Console.Write($"Enter last name (current: {employee.LastName}): ");
        string lastName = Console.ReadLine();
        if (!string.IsNullOrEmpty(lastName)) employee.LastName = lastName;

        Console.Write($"Enter email (current: {employee.Email}): ");
        string email = Console.ReadLine();
        if (!string.IsNullOrEmpty(email)) employee.Email = email;

        Console.Write($"Enter phone (current: {employee.Phone}): ");
        string phone = Console.ReadLine();
        if (!string.IsNullOrEmpty(phone)) employee.Phone = phone;

        Console.Write($"Enter department (current: {employee.Department}): ");
        string department = Console.ReadLine();
        if (!string.IsNullOrEmpty(department)) employee.Department = department;

        Console.Write($"Enter salary (current: {employee.Salary:C}): ");
        if (decimal.TryParse(Console.ReadLine(), out decimal salary)) employee.Salary = salary;

        Console.Write($"Enter hire date (current: {employee.HireDate:yyyy-MM-dd}): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime hireDate)) employee.HireDate = hireDate;

        manager.Update(employee);
    }

    private static void DeleteEmployee(EmployeeManager manager)
    {
        Console.Write("Enter employee ID to delete: ");
        if (int.TryParse(Console.ReadLine(), out int employeeId))
        {
            manager.Delete(employeeId);
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }
    }

    private static void SearchByName(EmployeeManager manager)
    {
        Console.Write("Enter name to search: ");
        string name = Console.ReadLine();

        List<Employee> results = manager.GetByNameContains(name);

        if (results.Count == 0)
        {
            Console.WriteLine("No employees found.");
            return;
        }

        Console.WriteLine("\n========== Search Results ==========");
        foreach (var emp in results)
        {
            manager.DisplayEmployee(emp);
        }
    }

    private static void ViewByDepartment(EmployeeManager manager)
    {
        Console.Write("Enter department: ");
        string department = Console.ReadLine();

        List<Employee> employees = manager.GetByDepartment(department);

        if (employees.Count == 0)
        {
            Console.WriteLine("No employees found in this department.");
            return;
        }

        Console.WriteLine($"\n========== Employees in {department} Department ==========");
        foreach (var emp in employees)
        {
            Console.WriteLine($"{emp.EmployeeId}. {emp.FirstName} {emp.LastName} - {emp.Salary:C}");
        }
        Console.WriteLine();
    }
}
