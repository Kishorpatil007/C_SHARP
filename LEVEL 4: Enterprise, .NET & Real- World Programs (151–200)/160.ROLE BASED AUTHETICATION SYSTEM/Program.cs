using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

public enum Role
{
    Admin,
    Manager,
    User
}

public enum Permission
{
    ViewReports,
    CreateReports,
    ManageUsers,
    EditSettings,
    ViewDashboard,
    ApproveRequests,
    SubmitRequests
}

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsActive { get; set; }
}

public class RolePermission
{
    private static readonly Dictionary<Role, List<Permission>> RolePermissions = new()
    {
        {
            Role.Admin, new List<Permission>
            {
                Permission.ViewReports,
                Permission.CreateReports,
                Permission.ManageUsers,
                Permission.EditSettings,
                Permission.ViewDashboard,
                Permission.ApproveRequests,
                Permission.SubmitRequests
            }
        },
        {
            Role.Manager, new List<Permission>
            {
                Permission.ViewReports,
                Permission.CreateReports,
                Permission.ViewDashboard,
                Permission.ApproveRequests,
                Permission.SubmitRequests
            }
        },
        {
            Role.User, new List<Permission>
            {
                Permission.ViewReports,
                Permission.ViewDashboard,
                Permission.SubmitRequests
            }
        }
    };

    public static bool HasPermission(Role role, Permission permission)
    {
        return RolePermissions.ContainsKey(role) && RolePermissions[role].Contains(permission);
    }

    public static List<Permission> GetRolePermissions(Role role)
    {
        return RolePermissions.ContainsKey(role) ? RolePermissions[role] : new List<Permission>();
    }
}

public class PasswordManager
{
    public static string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }

    public static bool VerifyPassword(string password, string hash)
    {
        var hashOfInput = HashPassword(password);
        return hashOfInput.Equals(hash);
    }
}

public class AuthenticationService
{
    private List<User> users = new List<User>();
    private int nextUserId = 1;

    public AuthenticationService()
    {
        InitializeDefaultUsers();
    }

    private void InitializeDefaultUsers()
    {
        users.Add(new User
        {
            UserId = nextUserId++,
            Username = "admin",
            PasswordHash = PasswordManager.HashPassword("admin123"),
            Email = "admin@system.com",
            Role = Role.Admin,
            CreatedDate = DateTime.Now,
            IsActive = true
        });

        users.Add(new User
        {
            UserId = nextUserId++,
            Username = "manager",
            PasswordHash = PasswordManager.HashPassword("manager123"),
            Email = "manager@system.com",
            Role = Role.Manager,
            CreatedDate = DateTime.Now,
            IsActive = true
        });

        users.Add(new User
        {
            UserId = nextUserId++,
            Username = "user",
            PasswordHash = PasswordManager.HashPassword("user123"),
            Email = "user@system.com",
            Role = Role.User,
            CreatedDate = DateTime.Now,
            IsActive = true
        });
    }

    public User Login(string username, string password)
    {
        var user = users.FirstOrDefault(u => u.Username == username && u.IsActive);

        if (user == null)
        {
            Console.WriteLine("Invalid username or user not found.");
            return null;
        }

        if (!PasswordManager.VerifyPassword(password, user.PasswordHash))
        {
            Console.WriteLine("Invalid password.");
            return null;
        }

        Console.WriteLine($"Login successful. Welcome, {user.Username}!");
        return user;
    }

    public bool Register(string username, string password, string email)
    {
        if (users.Any(u => u.Username == username))
        {
            Console.WriteLine("Username already exists.");
            return false;
        }

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            Console.WriteLine("Username and password cannot be empty.");
            return false;
        }

        users.Add(new User
        {
            UserId = nextUserId++,
            Username = username,
            PasswordHash = PasswordManager.HashPassword(password),
            Email = email,
            Role = Role.User,
            CreatedDate = DateTime.Now,
            IsActive = true
        });

        Console.WriteLine("Registration successful. You have been assigned the User role.");
        return true;
    }

    public void DisplayAllUsers(User currentUser)
    {
        if (!RolePermission.HasPermission(currentUser.Role, Permission.ManageUsers))
        {
            Console.WriteLine("Access Denied: You don't have permission to view users.");
            return;
        }

        Console.WriteLine("\n========== User List ==========");
        Console.WriteLine("{0,-5} {1,-15} {2,-25} {3,-12} {4,-15}", "ID", "Username", "Email", "Role", "Status");
        Console.WriteLine(new string('-', 72));

        foreach (var user in users)
        {
            Console.WriteLine("{0,-5} {1,-15} {2,-25} {3,-12} {4,-15}", user.UserId, user.Username, user.Email, user.Role, user.IsActive ? "Active" : "Inactive");
        }
        Console.WriteLine();
    }

    public void DeactivateUser(User currentUser, int userId)
    {
        if (!RolePermission.HasPermission(currentUser.Role, Permission.ManageUsers))
        {
            Console.WriteLine("Access Denied: You don't have permission to manage users.");
            return;
        }

        var user = users.FirstOrDefault(u => u.UserId == userId);
        if (user == null)
        {
            Console.WriteLine("User not found.");
            return;
        }

        user.IsActive = false;
        Console.WriteLine($"User {user.Username} has been deactivated.");
    }

    public void ChangeUserRole(User currentUser, int userId, Role newRole)
    {
        if (!RolePermission.HasPermission(currentUser.Role, Permission.ManageUsers))
        {
            Console.WriteLine("Access Denied: You don't have permission to manage users.");
            return;
        }

        var user = users.FirstOrDefault(u => u.UserId == userId);
        if (user == null)
        {
            Console.WriteLine("User not found.");
            return;
        }

        user.Role = newRole;
        Console.WriteLine($"User {user.Username} role changed to {newRole}.");
    }
}

public class ReportService
{
    public void ViewReports(User currentUser)
    {
        if (!RolePermission.HasPermission(currentUser.Role, Permission.ViewReports))
        {
            Console.WriteLine("Access Denied: You don't have permission to view reports.");
            return;
        }

        Console.WriteLine("\n========== Reports ==========");
        Console.WriteLine("1. Sales Report");
        Console.WriteLine("2. Performance Report");
        Console.WriteLine("3. User Activity Report");
        Console.WriteLine("4. Back");
        Console.Write("Choose a report: ");

        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                Console.WriteLine("Sales Report: Total Sales = $50,000 | Growth = 15%");
                break;
            case "2":
                Console.WriteLine("Performance Report: Efficiency = 92% | Target Met = Yes");
                break;
            case "3":
                if (RolePermission.HasPermission(currentUser.Role, Permission.ManageUsers))
                {
                    Console.WriteLine("User Activity: 250 active users | 50 new registrations");
                }
                else
                {
                    Console.WriteLine("Access Denied: This report requires admin access.");
                }
                break;
        }
    }

    public void CreateReport(User currentUser)
    {
        if (!RolePermission.HasPermission(currentUser.Role, Permission.CreateReports))
        {
            Console.WriteLine("Access Denied: You don't have permission to create reports.");
            return;
        }

        Console.Write("Enter report title: ");
        string title = Console.ReadLine();

        Console.Write("Enter report description: ");
        string description = Console.ReadLine();

        Console.WriteLine($"\nReport '{title}' created successfully!");
        Console.WriteLine($"Description: {description}");
        Console.WriteLine($"Created by: {currentUser.Username}");
        Console.WriteLine($"Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
    }
}

public class RequestService
{
    private List<string> requests = new List<string>();

    public void SubmitRequest(User currentUser)
    {
        if (!RolePermission.HasPermission(currentUser.Role, Permission.SubmitRequests))
        {
            Console.WriteLine("Access Denied: You don't have permission to submit requests.");
            return;
        }

        Console.Write("Enter your request: ");
        string request = Console.ReadLine();

        requests.Add($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {currentUser.Username}: {request}");
        Console.WriteLine("Request submitted successfully.");
    }

    public void ApproveRequest(User currentUser)
    {
        if (!RolePermission.HasPermission(currentUser.Role, Permission.ApproveRequests))
        {
            Console.WriteLine("Access Denied: You don't have permission to approve requests.");
            return;
        }

        if (requests.Count == 0)
        {
            Console.WriteLine("No pending requests.");
            return;
        }

        Console.WriteLine("\n========== Pending Requests ==========");
        for (int i = 0; i < requests.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {requests[i]}");
        }

        Console.Write("Enter request number to approve (0 to cancel): ");
        if (int.TryParse(Console.ReadLine(), out int requestNum) && requestNum > 0 && requestNum <= requests.Count)
        {
            Console.WriteLine($"Request approved: {requests[requestNum - 1]}");
            requests.RemoveAt(requestNum - 1);
        }
    }

    public void ViewRequests(User currentUser)
    {
        if (requests.Count == 0)
        {
            Console.WriteLine("No requests available.");
            return;
        }

        Console.WriteLine("\n========== All Requests ==========");
        foreach (var req in requests)
        {
            Console.WriteLine(req);
        }
    }
}

public class DashboardService
{
    public void DisplayDashboard(User currentUser)
    {
        if (!RolePermission.HasPermission(currentUser.Role, Permission.ViewDashboard))
        {
            Console.WriteLine("Access Denied: You don't have permission to view the dashboard.");
            return;
        }

        Console.WriteLine("\n========== Dashboard ==========");
        Console.WriteLine($"Welcome, {currentUser.Username}!");
        Console.WriteLine($"Role: {currentUser.Role}");
        Console.WriteLine($"Email: {currentUser.Email}");
        Console.WriteLine($"Member Since: {currentUser.CreatedDate:yyyy-MM-dd}");

        Console.WriteLine("\nYour Permissions:");
        var permissions = RolePermission.GetRolePermissions(currentUser.Role);
        foreach (var perm in permissions)
        {
            Console.WriteLine($"  - {perm}");
        }
        Console.WriteLine();
    }

    public void EditSettings(User currentUser)
    {
        if (!RolePermission.HasPermission(currentUser.Role, Permission.EditSettings))
        {
            Console.WriteLine("Access Denied: You don't have permission to edit settings.");
            return;
        }

        Console.WriteLine("\n========== Settings ==========");
        Console.WriteLine("1. Change Email");
        Console.WriteLine("2. Change Password");
        Console.WriteLine("3. System Configuration (Admin Only)");
        Console.WriteLine("4. Back");
        Console.Write("Choose an option: ");

        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                Console.Write("Enter new email: ");
                string email = Console.ReadLine();
                Console.WriteLine($"Email changed to: {email}");
                break;
            case "2":
                Console.Write("Enter new password: ");
                string password = Console.ReadLine();
                Console.WriteLine("Password changed successfully.");
                break;
            case "3":
                if (currentUser.Role == Role.Admin)
                {
                    Console.WriteLine("System Configuration: All systems operational");
                }
                else
                {
                    Console.WriteLine("Access Denied: Admin only.");
                }
                break;
        }
    }
}

public class Program
{
    private static AuthenticationService authService = new AuthenticationService();
    private static ReportService reportService = new ReportService();
    private static RequestService requestService = new RequestService();
    private static DashboardService dashboardService = new DashboardService();
    private static User currentUser = null;

    public static void Main()
    {
        Console.WriteLine("========== Role-Based Authentication System ==========");
        Console.WriteLine("Default Credentials:");
        Console.WriteLine("  Admin: admin / admin123");
        Console.WriteLine("  Manager: manager / manager123");
        Console.WriteLine("  User: user / user123");
        Console.WriteLine();

        bool running = true;

        while (running)
        {
            if (currentUser == null)
            {
                DisplayAuthMenu();
            }
            else
            {
                DisplayMainMenu();
            }

            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            if (currentUser == null)
            {
                HandleAuthChoice(choice, ref running);
            }
            else
            {
                HandleMainChoice(choice, ref running);
            }
        }

        Console.WriteLine("Thank you for using the system. Goodbye!");
    }

    private static void DisplayAuthMenu()
    {
        Console.WriteLine("\n========== Authentication ==========");
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Register");
        Console.WriteLine("3. Exit");
    }

    private static void HandleAuthChoice(string choice, ref bool running)
    {
        switch (choice)
        {
            case "1":
                HandleLogin();
                break;
            case "2":
                HandleRegister();
                break;
            case "3":
                running = false;
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    private static void HandleLogin()
    {
        Console.Write("Enter username: ");
        string username = Console.ReadLine();

        Console.Write("Enter password: ");
        string password = Console.ReadLine();

        currentUser = authService.Login(username, password);
    }

    private static void HandleRegister()
    {
        Console.Write("Enter username: ");
        string username = Console.ReadLine();

        Console.Write("Enter email: ");
        string email = Console.ReadLine();

        Console.Write("Enter password: ");
        string password = Console.ReadLine();

        authService.Register(username, password, email);
    }

    private static void DisplayMainMenu()
    {
        Console.WriteLine($"\n========== Main Menu (Logged in as: {currentUser.Username} - {currentUser.Role}) ==========");
        Console.WriteLine("1. Dashboard");
        Console.WriteLine("2. View Reports");
        Console.WriteLine("3. Create Report");
        Console.WriteLine("4. Submit Request");
        Console.WriteLine("5. View Requests");
        Console.WriteLine("6. Approve Request");
        Console.WriteLine("7. Edit Settings");
        Console.WriteLine("8. Manage Users (Admin Only)");
        Console.WriteLine("9. Logout");
    }

    private static void HandleMainChoice(string choice, ref bool running)
    {
        switch (choice)
        {
            case "1":
                dashboardService.DisplayDashboard(currentUser);
                break;
            case "2":
                reportService.ViewReports(currentUser);
                break;
            case "3":
                reportService.CreateReport(currentUser);
                break;
            case "4":
                requestService.SubmitRequest(currentUser);
                break;
            case "5":
                requestService.ViewRequests(currentUser);
                break;
            case "6":
                requestService.ApproveRequest(currentUser);
                break;
            case "7":
                dashboardService.EditSettings(currentUser);
                break;
            case "8":
                if (currentUser.Role == Role.Admin)
                {
                    ManageUsers();
                }
                else
                {
                    Console.WriteLine("Access Denied: This option is for administrators only.");
                }
                break;
            case "9":
                currentUser = null;
                Console.WriteLine("Logged out successfully.");
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    private static void ManageUsers()
    {
        Console.WriteLine("\n========== User Management ==========");
        Console.WriteLine("1. View All Users");
        Console.WriteLine("2. Deactivate User");
        Console.WriteLine("3. Change User Role");
        Console.WriteLine("4. Back");
        Console.Write("Choose an option: ");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                authService.DisplayAllUsers(currentUser);
                break;
            case "2":
                Console.Write("Enter user ID to deactivate: ");
                if (int.TryParse(Console.ReadLine(), out int userId))
                {
                    authService.DeactivateUser(currentUser, userId);
                }
                break;
            case "3":
                Console.Write("Enter user ID: ");
                if (int.TryParse(Console.ReadLine(), out int uid))
                {
                    Console.WriteLine("Enter new role (0=Admin, 1=Manager, 2=User): ");
                    if (int.TryParse(Console.ReadLine(), out int roleNum) && roleNum >= 0 && roleNum <= 2)
                    {
                        Role newRole = (Role)roleNum;
                        authService.ChangeUserRole(currentUser, uid, newRole);
                    }
                }
                break;
        }
    }
}
