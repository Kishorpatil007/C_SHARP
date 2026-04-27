using System;
using System.Collections.Generic;
using System.Linq;

enum EmploymentType
{
    FullTime,
    PartTime,
    Contract
}

enum PayrollStatus
{
    Pending,
    Processed,
    Paid,
    Cancelled
}

class Deduction
{
    public string DeductionName { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }

    public Deduction(string name, decimal amount, string description)
    {
        DeductionName = name;
        Amount = amount;
        Description = description;
    }

    public override string ToString()
    {
        return $"{DeductionName}: ${Amount:F2}";
    }
}

class PayrollRecord
{
    public int PayrollId { get; set; }
    public int EmployeeId { get; set; }
    public DateTime PayPeriodStart { get; set; }
    public DateTime PayPeriodEnd { get; set; }
    public DateTime ProcessedDate { get; set; }
    public decimal BasicSalary { get; set; }
    public decimal HoursWorked { get; set; }
    public decimal Bonus { get; set; }
    public List<Deduction> Deductions { get; set; }
    public PayrollStatus Status { get; set; }
    public decimal GrossSalary { get; set; }
    public decimal TotalDeductions { get; set; }
    public decimal NetSalary { get; set; }

    public PayrollRecord(int payrollId, int employeeId, decimal basicSalary, decimal hoursWorked = 160)
    {
        PayrollId = payrollId;
        EmployeeId = employeeId;
        PayPeriodStart = DateTime.Now.AddDays(-15);
        PayPeriodEnd = DateTime.Now;
        ProcessedDate = DateTime.Now;
        BasicSalary = basicSalary;
        HoursWorked = hoursWorked;
        Bonus = 0;
        Deductions = new List<Deduction>();
        Status = PayrollStatus.Pending;
    }

    public void CalculateSalary()
    {
        GrossSalary = BasicSalary + Bonus;
        TotalDeductions = Deductions.Sum(d => d.Amount);
        NetSalary = GrossSalary - TotalDeductions;
    }

    public override string ToString()
    {
        return $"ID: {PayrollId} | Employee: {EmployeeId} | Gross: ${GrossSalary:F2} | Deductions: ${TotalDeductions:F2} | Net: ${NetSalary:F2} | Status: {Status}";
    }
}

class Employee
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Department { get; set; }
    public EmploymentType EmploymentType { get; set; }
    public decimal BasicSalary { get; set; }
    public decimal HourlyRate { get; set; }
    public DateTime JoinDate { get; set; }
    public bool IsActive { get; set; }
    public List<PayrollRecord> PayrollHistory { get; set; }
    public decimal TaxPercentage { get; set; }

    public Employee(int employeeId, string firstName, string lastName, string email, string phoneNumber, 
                    string department, EmploymentType empType, decimal basicSalary, decimal hourlyRate = 0)
    {
        EmployeeId = employeeId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Department = department;
        EmploymentType = empType;
        BasicSalary = basicSalary;
        HourlyRate = hourlyRate;
        JoinDate = DateTime.Now;
        IsActive = true;
        PayrollHistory = new List<PayrollRecord>();
        TaxPercentage = 0.15m;
    }

    public override string ToString()
    {
        return $"ID: {EmployeeId} | {FirstName} {LastName} | Dept: {Department} | Type: {EmploymentType} | Salary: ${BasicSalary:F2} | Status: {(IsActive ? "Active" : "Inactive")}";
    }
}

class PayrollSystem
{
    private List<Employee> employees = new List<Employee>();
    private List<PayrollRecord> payrollRecords = new List<PayrollRecord>();
    private int nextEmployeeId = 5001;
    private int nextPayrollId = 1;

    public void AddEmployee(string firstName, string lastName, string email, string phoneNumber, 
                            string department, EmploymentType empType, decimal basicSalary, decimal hourlyRate = 0)
    {
        if (employees.Any(e => e.Email == email))
        {
            Console.WriteLine("Employee with this email already exists!");
            return;
        }

        var employee = new Employee(nextEmployeeId++, firstName, lastName, email, phoneNumber, 
                                    department, empType, basicSalary, hourlyRate);
        employees.Add(employee);
        Console.WriteLine($"Employee added successfully! Employee ID: {employee.EmployeeId}");
    }

    public void RemoveEmployee(int employeeId)
    {
        var employee = employees.FirstOrDefault(e => e.EmployeeId == employeeId);
        if (employee == null)
        {
            Console.WriteLine("Employee not found!");
            return;
        }

        employee.IsActive = false;
        Console.WriteLine("Employee deactivated successfully!");
    }

    public void UpdateEmployeeSalary(int employeeId, decimal newSalary)
    {
        var employee = employees.FirstOrDefault(e => e.EmployeeId == employeeId);
        if (employee == null)
        {
            Console.WriteLine("Employee not found!");
            return;
        }

        decimal oldSalary = employee.BasicSalary;
        employee.BasicSalary = newSalary;
        Console.WriteLine($"Salary updated! Old: ${oldSalary:F2}, New: ${newSalary:F2}");
    }

    public void UpdateEmployeeTaxRate(int employeeId, decimal taxPercentage)
    {
        var employee = employees.FirstOrDefault(e => e.EmployeeId == employeeId);
        if (employee == null)
        {
            Console.WriteLine("Employee not found!");
            return;
        }

        if (taxPercentage < 0 || taxPercentage > 1)
        {
            Console.WriteLine("Tax percentage must be between 0 and 1!");
            return;
        }

        employee.TaxPercentage = taxPercentage;
        Console.WriteLine($"Tax rate updated to {taxPercentage * 100}%");
    }

    public void ProcessPayroll(int employeeId, decimal hoursWorked = 160, decimal bonus = 0)
    {
        var employee = employees.FirstOrDefault(e => e.EmployeeId == employeeId);
        if (employee == null)
        {
            Console.WriteLine("Employee not found!");
            return;
        }

        if (!employee.IsActive)
        {
            Console.WriteLine("Cannot process payroll for inactive employee!");
            return;
        }

        var payroll = new PayrollRecord(nextPayrollId++, employeeId, employee.BasicSalary, hoursWorked);
        payroll.Bonus = bonus;

        decimal incomeTax = payroll.BasicSalary * employee.TaxPercentage;
        payroll.Deductions.Add(new Deduction("Income Tax", incomeTax, "Federal Income Tax"));

        decimal socialSecurity = payroll.BasicSalary * 0.062m;
        payroll.Deductions.Add(new Deduction("Social Security", socialSecurity, "Social Security Contribution"));

        decimal medicare = payroll.BasicSalary * 0.0145m;
        payroll.Deductions.Add(new Deduction("Medicare", medicare, "Medicare Contribution"));

        decimal healthInsurance = 200;
        if (employee.EmploymentType != EmploymentType.PartTime)
        {
            payroll.Deductions.Add(new Deduction("Health Insurance", healthInsurance, "Health Insurance Premium"));
        }

        payroll.CalculateSalary();
        payroll.Status = PayrollStatus.Processed;

        payrollRecords.Add(payroll);
        employee.PayrollHistory.Add(payroll);
        Console.WriteLine($"Payroll processed! Payroll ID: {payroll.PayrollId}, Net Salary: ${payroll.NetSalary:F2}");
    }

    public void AddManualDeduction(int payrollId, string deductionName, decimal amount, string description)
    {
        var payroll = payrollRecords.FirstOrDefault(p => p.PayrollId == payrollId);
        if (payroll == null)
        {
            Console.WriteLine("Payroll record not found!");
            return;
        }

        if (payroll.Status == PayrollStatus.Paid || payroll.Status == PayrollStatus.Cancelled)
        {
            Console.WriteLine("Cannot modify paid or cancelled payroll!");
            return;
        }

        payroll.Deductions.Add(new Deduction(deductionName, amount, description));
        payroll.CalculateSalary();
        Console.WriteLine($"Deduction added! New Net Salary: ${payroll.NetSalary:F2}");
    }

    public void MarkPayrollAsPaid(int payrollId)
    {
        var payroll = payrollRecords.FirstOrDefault(p => p.PayrollId == payrollId);
        if (payroll == null)
        {
            Console.WriteLine("Payroll record not found!");
            return;
        }

        if (payroll.Status == PayrollStatus.Paid)
        {
            Console.WriteLine("Payroll already marked as paid!");
            return;
        }

        payroll.Status = PayrollStatus.Paid;
        payroll.ProcessedDate = DateTime.Now;
        Console.WriteLine($"Payroll marked as paid! Amount: ${payroll.NetSalary:F2}");
    }

    public void ViewEmployeePayroll(int employeeId)
    {
        var employee = employees.FirstOrDefault(e => e.EmployeeId == employeeId);
        if (employee == null)
        {
            Console.WriteLine("Employee not found!");
            return;
        }

        if (employee.PayrollHistory.Count == 0)
        {
            Console.WriteLine("No payroll records for this employee!");
            return;
        }

        Console.WriteLine($"\n========== Payroll History for Employee {employeeId} - {employee.FirstName} {employee.LastName} ==========");
        foreach (var payroll in employee.PayrollHistory.OrderByDescending(p => p.ProcessedDate))
        {
            Console.WriteLine(payroll);
            Console.WriteLine("  Deductions:");
            foreach (var deduction in payroll.Deductions)
            {
                Console.WriteLine($"    - {deduction}");
            }
        }
        Console.WriteLine("================================================================================\n");
    }

    public void ViewPayrollDetails(int payrollId)
    {
        var payroll = payrollRecords.FirstOrDefault(p => p.PayrollId == payrollId);
        if (payroll == null)
        {
            Console.WriteLine("Payroll record not found!");
            return;
        }

        var employee = employees.FirstOrDefault(e => e.EmployeeId == payroll.EmployeeId);

        Console.WriteLine("\n========== Payroll Details ==========");
        Console.WriteLine($"Payroll ID: {payroll.PayrollId}");
        Console.WriteLine($"Employee: {employee.FirstName} {employee.LastName} (ID: {employee.EmployeeId})");
        Console.WriteLine($"Pay Period: {payroll.PayPeriodStart:yyyy-MM-dd} to {payroll.PayPeriodEnd:yyyy-MM-dd}");
        Console.WriteLine($"Basic Salary: ${payroll.BasicSalary:F2}");
        Console.WriteLine($"Bonus: ${payroll.Bonus:F2}");
        Console.WriteLine($"Gross Salary: ${payroll.GrossSalary:F2}");
        Console.WriteLine("\nDeductions:");
        foreach (var deduction in payroll.Deductions)
        {
            Console.WriteLine($"  {deduction.DeductionName}: ${deduction.Amount:F2}");
        }
        Console.WriteLine($"\nTotal Deductions: ${payroll.TotalDeductions:F2}");
        Console.WriteLine($"Net Salary: ${payroll.NetSalary:F2}");
        Console.WriteLine($"Status: {payroll.Status}");
        Console.WriteLine("====================================\n");
    }

    public void ViewAllEmployees()
    {
        if (employees.Count == 0)
        {
            Console.WriteLine("No employees in the system!");
            return;
        }

        Console.WriteLine("\n========== All Employees ==========");
        foreach (var employee in employees.Where(e => e.IsActive))
        {
            Console.WriteLine(employee);
        }
        Console.WriteLine("===================================\n");
    }

    public void ViewDepartmentSummary(string department)
    {
        var deptEmployees = employees.Where(e => e.Department == department && e.IsActive).ToList();
        if (deptEmployees.Count == 0)
        {
            Console.WriteLine($"No employees in {department} department!");
            return;
        }

        Console.WriteLine($"\n========== {department} Department Summary ==========");
        Console.WriteLine($"Total Employees: {deptEmployees.Count}");
        decimal totalSalary = deptEmployees.Sum(e => e.BasicSalary);
        Console.WriteLine($"Total Salary: ${totalSalary:F2}");
        Console.WriteLine($"Average Salary: ${totalSalary / deptEmployees.Count:F2}");
        Console.WriteLine("\nEmployees:");
        foreach (var emp in deptEmployees)
        {
            Console.WriteLine($"  {emp.FirstName} {emp.LastName}: ${emp.BasicSalary:F2}");
        }
        Console.WriteLine("=====================================================\n");
    }

    public void GeneratePayrollReport()
    {
        if (payrollRecords.Count == 0)
        {
            Console.WriteLine("No payroll records!");
            return;
        }

        var pendingPayroll = payrollRecords.Where(p => p.Status == PayrollStatus.Pending).ToList();
        var processedPayroll = payrollRecords.Where(p => p.Status == PayrollStatus.Processed).ToList();
        var paidPayroll = payrollRecords.Where(p => p.Status == PayrollStatus.Paid).ToList();

        Console.WriteLine("\n========== Payroll Report ==========");
        Console.WriteLine($"Total Payroll Records: {payrollRecords.Count}");
        Console.WriteLine($"Pending: {pendingPayroll.Count}");
        Console.WriteLine($"Processed: {processedPayroll.Count}");
        Console.WriteLine($"Paid: {paidPayroll.Count}");
        Console.WriteLine($"\nTotal Gross Payroll: ${payrollRecords.Sum(p => p.GrossSalary):F2}");
        Console.WriteLine($"Total Deductions: ${payrollRecords.Sum(p => p.TotalDeductions):F2}");
        Console.WriteLine($"Total Net Payroll: ${payrollRecords.Sum(p => p.NetSalary):F2}");
        Console.WriteLine("\nDeduction Breakdown:");
        
        var allDeductions = payrollRecords.SelectMany(p => p.Deductions).GroupBy(d => d.DeductionName);
        foreach (var deductionGroup in allDeductions)
        {
            decimal total = deductionGroup.Sum(d => d.Amount);
            Console.WriteLine($"  {deductionGroup.Key}: ${total:F2}");
        }
        Console.WriteLine("===================================\n");
    }

    public void GenerateEmployeeSalaryReport()
    {
        if (employees.Count == 0)
        {
            Console.WriteLine("No employees in the system!");
            return;
        }

        Console.WriteLine("\n========== Employee Salary Report ==========");
        decimal totalSalary = 0;
        foreach (var emp in employees.Where(e => e.IsActive).OrderByDescending(e => e.BasicSalary))
        {
            Console.WriteLine($"{emp.FirstName} {emp.LastName} ({emp.Department}): ${emp.BasicSalary:F2}");
            totalSalary += emp.BasicSalary;
        }
        Console.WriteLine($"\nTotal Monthly Salary Expense: ${totalSalary:F2}");
        Console.WriteLine($"Average Salary: ${totalSalary / employees.Count(e => e.IsActive):F2}");
        Console.WriteLine("===========================================\n");
    }
}

class Program
{
    static void Main()
    {
        PayrollSystem payroll = new PayrollSystem();

        payroll.AddEmployee("John", "Smith", "john.smith@company.com", "555-0101", "IT", EmploymentType.FullTime, 5000, 25);
        payroll.AddEmployee("Sarah", "Johnson", "sarah.j@company.com", "555-0102", "HR", EmploymentType.FullTime, 4500, 22);
        payroll.AddEmployee("Mike", "Brown", "mike.brown@company.com", "555-0103", "Sales", EmploymentType.FullTime, 4000, 20);
        payroll.AddEmployee("Emily", "Davis", "emily.d@company.com", "555-0104", "IT", EmploymentType.PartTime, 2500, 15);
        payroll.AddEmployee("David", "Wilson", "d.wilson@company.com", "555-0105", "Finance", EmploymentType.FullTime, 5500, 28);

        bool running = true;
        while (running)
        {
            Console.WriteLine("\n================ Payroll Management System ================");
            Console.WriteLine("1. Add Employee");
            Console.WriteLine("2. Update Employee Salary");
            Console.WriteLine("3. Update Employee Tax Rate");
            Console.WriteLine("4. Remove Employee");
            Console.WriteLine("5. Process Payroll");
            Console.WriteLine("6. Add Manual Deduction");
            Console.WriteLine("7. Mark Payroll as Paid");
            Console.WriteLine("8. View Employee Payroll History");
            Console.WriteLine("9. View Payroll Details");
            Console.WriteLine("10. View All Employees");
            Console.WriteLine("11. View Department Summary");
            Console.WriteLine("12. Generate Payroll Report");
            Console.WriteLine("13. Generate Employee Salary Report");
            Console.WriteLine("14. Exit");
            Console.WriteLine("===========================================================");
            Console.Write("Enter your choice (1-14): ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        Console.Write("Enter First Name: ");
                        string firstName = Console.ReadLine();
                        Console.Write("Enter Last Name: ");
                        string lastName = Console.ReadLine();
                        Console.Write("Enter Email: ");
                        string email = Console.ReadLine();
                        Console.Write("Enter Phone Number: ");
                        string phone = Console.ReadLine();
                        Console.Write("Enter Department: ");
                        string department = Console.ReadLine();
                        Console.WriteLine("Employment Types: 0=FullTime, 1=PartTime, 2=Contract");
                        Console.Write("Enter Employment Type (0-2): ");
                        if (int.TryParse(Console.ReadLine(), out int empType) && empType >= 0 && empType <= 2)
                        {
                            Console.Write("Enter Basic Salary: ");
                            if (decimal.TryParse(Console.ReadLine(), out decimal salary) && salary > 0)
                            {
                                Console.Write("Enter Hourly Rate: ");
                                if (decimal.TryParse(Console.ReadLine(), out decimal hourlyRate) && hourlyRate >= 0)
                                {
                                    payroll.AddEmployee(firstName, lastName, email, phone, department, (EmploymentType)empType, salary, hourlyRate);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid hourly rate!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid salary!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid employment type!");
                        }
                        break;

                    case 2:
                        Console.Write("Enter Employee ID: ");
                        if (int.TryParse(Console.ReadLine(), out int empIdUpdate))
                        {
                            Console.Write("Enter New Salary: ");
                            if (decimal.TryParse(Console.ReadLine(), out decimal newSalary) && newSalary > 0)
                            {
                                payroll.UpdateEmployeeSalary(empIdUpdate, newSalary);
                            }
                            else
                            {
                                Console.WriteLine("Invalid salary!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Employee ID!");
                        }
                        break;

                    case 3:
                        Console.Write("Enter Employee ID: ");
                        if (int.TryParse(Console.ReadLine(), out int empIdTax))
                        {
                            Console.Write("Enter Tax Rate (0-1, e.g., 0.15 for 15%): ");
                            if (decimal.TryParse(Console.ReadLine(), out decimal taxRate))
                            {
                                payroll.UpdateEmployeeTaxRate(empIdTax, taxRate);
                            }
                            else
                            {
                                Console.WriteLine("Invalid tax rate!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Employee ID!");
                        }
                        break;

                    case 4:
                        Console.Write("Enter Employee ID: ");
                        if (int.TryParse(Console.ReadLine(), out int empIdRemove))
                        {
                            payroll.RemoveEmployee(empIdRemove);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Employee ID!");
                        }
                        break;

                    case 5:
                        Console.Write("Enter Employee ID: ");
                        if (int.TryParse(Console.ReadLine(), out int empIdProcess))
                        {
                            Console.Write("Enter Hours Worked (default 160): ");
                            if (decimal.TryParse(Console.ReadLine(), out decimal hoursWorked) && hoursWorked >= 0)
                            {
                                Console.Write("Enter Bonus (default 0): ");
                                if (decimal.TryParse(Console.ReadLine(), out decimal bonus) && bonus >= 0)
                                {
                                    payroll.ProcessPayroll(empIdProcess, hoursWorked, bonus);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid bonus!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid hours worked!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Employee ID!");
                        }
                        break;

                    case 6:
                        Console.Write("Enter Payroll ID: ");
                        if (int.TryParse(Console.ReadLine(), out int payrollIdDeduct))
                        {
                            Console.Write("Enter Deduction Name: ");
                            string deductName = Console.ReadLine();
                            Console.Write("Enter Deduction Amount: ");
                            if (decimal.TryParse(Console.ReadLine(), out decimal deductAmount) && deductAmount > 0)
                            {
                                Console.Write("Enter Description: ");
                                string deductDesc = Console.ReadLine();
                                payroll.AddManualDeduction(payrollIdDeduct, deductName, deductAmount, deductDesc);
                            }
                            else
                            {
                                Console.WriteLine("Invalid deduction amount!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Payroll ID!");
                        }
                        break;

                    case 7:
                        Console.Write("Enter Payroll ID: ");
                        if (int.TryParse(Console.ReadLine(), out int payrollIdPaid))
                        {
                            payroll.MarkPayrollAsPaid(payrollIdPaid);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Payroll ID!");
                        }
                        break;

                    case 8:
                        Console.Write("Enter Employee ID: ");
                        if (int.TryParse(Console.ReadLine(), out int empIdHistory))
                        {
                            payroll.ViewEmployeePayroll(empIdHistory);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Employee ID!");
                        }
                        break;

                    case 9:
                        Console.Write("Enter Payroll ID: ");
                        if (int.TryParse(Console.ReadLine(), out int payrollIdView))
                        {
                            payroll.ViewPayrollDetails(payrollIdView);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Payroll ID!");
                        }
                        break;

                    case 10:
                        payroll.ViewAllEmployees();
                        break;

                    case 11:
                        Console.Write("Enter Department Name: ");
                        string deptName = Console.ReadLine();
                        payroll.ViewDepartmentSummary(deptName);
                        break;

                    case 12:
                        payroll.GeneratePayrollReport();
                        break;

                    case 13:
                        payroll.GenerateEmployeeSalaryReport();
                        break;

                    case 14:
                        running = false;
                        Console.WriteLine("Thank you for using Payroll Management System!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice! Please enter a number between 1 and 14.");
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
