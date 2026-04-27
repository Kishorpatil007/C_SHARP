using System;
using System.Collections.Generic;
using System.Linq;

enum AccountType
{
    Savings,
    Checking,
    Business
}

enum TransactionType
{
    Deposit,
    Withdrawal,
    Transfer,
    Interest,
    Fee
}

class Transaction
{
    public int TransactionId { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Description { get; set; }
    public string RelatedAccountNumber { get; set; }

    public Transaction(int transactionId, TransactionType type, decimal amount, string description, string relatedAccount = "")
    {
        TransactionId = transactionId;
        Type = type;
        Amount = amount;
        TransactionDate = DateTime.Now;
        Description = description;
        RelatedAccountNumber = relatedAccount;
    }

    public override string ToString()
    {
        return $"ID: {TransactionId} | Type: {Type} | Amount: ${Amount:F2} | Date: {TransactionDate:yyyy-MM-dd HH:mm:ss} | {Description}";
    }
}

class BankAccount
{
    public string AccountNumber { get; set; }
    public string CustomerName { get; set; }
    public AccountType AccountType { get; set; }
    public decimal Balance { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public List<Transaction> Transactions { get; set; }
    public decimal InterestRate { get; set; }
    public decimal MinimumBalance { get; set; }
    public bool OverdraftProtection { get; set; }

    public BankAccount(string accountNumber, string customerName, AccountType accountType, decimal initialDeposit)
    {
        AccountNumber = accountNumber;
        CustomerName = customerName;
        AccountType = accountType;
        Balance = initialDeposit;
        IsActive = true;
        CreatedDate = DateTime.Now;
        Transactions = new List<Transaction>();
        OverdraftProtection = true;

        if (accountType == AccountType.Savings)
        {
            InterestRate = 0.03m;
            MinimumBalance = 100;
        }
        else if (accountType == AccountType.Checking)
        {
            InterestRate = 0.001m;
            MinimumBalance = 50;
        }
        else
        {
            InterestRate = 0.02m;
            MinimumBalance = 500;
        }

        Transactions.Add(new Transaction(1, TransactionType.Deposit, initialDeposit, "Initial Deposit"));
    }

    public override string ToString()
    {
        return $"Account: {AccountNumber} | Name: {CustomerName} | Type: {AccountType} | Balance: ${Balance:F2} | Status: {(IsActive ? "Active" : "Inactive")}";
    }
}

class Customer
{
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public DateTime RegistrationDate { get; set; }
    public List<string> Accounts { get; set; }

    public Customer(int customerId, string name, string email, string phoneNumber, string address)
    {
        CustomerId = customerId;
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
        RegistrationDate = DateTime.Now;
        Accounts = new List<string>();
    }

    public override string ToString()
    {
        return $"ID: {CustomerId} | Name: {Name} | Email: {Email} | Phone: {PhoneNumber} | Accounts: {Accounts.Count}";
    }
}

class BankingSystem
{
    private List<BankAccount> accounts = new List<BankAccount>();
    private List<Customer> customers = new List<Customer>();
    private int nextCustomerId = 10001;
    private int nextTransactionId = 1;

    private string GenerateAccountNumber()
    {
        return "ACC" + DateTime.Now.Ticks.ToString().Substring(0, 12);
    }

    public void RegisterCustomer(string name, string email, string phoneNumber, string address)
    {
        var customer = new Customer(nextCustomerId++, name, email, phoneNumber, address);
        customers.Add(customer);
        Console.WriteLine($"Customer registered successfully! Customer ID: {customer.CustomerId}");
    }

    public void CreateAccount(int customerId, AccountType accountType, decimal initialDeposit)
    {
        var customer = customers.FirstOrDefault(c => c.CustomerId == customerId);
        if (customer == null)
        {
            Console.WriteLine("Customer not found!");
            return;
        }

        if (initialDeposit <= 0)
        {
            Console.WriteLine("Initial deposit must be greater than 0!");
            return;
        }

        string accountNumber = GenerateAccountNumber();
        var account = new BankAccount(accountNumber, customer.Name, accountType, initialDeposit);
        accounts.Add(account);
        customer.Accounts.Add(accountNumber);
        Console.WriteLine($"Account created successfully! Account Number: {accountNumber}");
    }

    public void Deposit(string accountNumber, decimal amount)
    {
        var account = accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
        if (account == null)
        {
            Console.WriteLine("Account not found!");
            return;
        }

        if (!account.IsActive)
        {
            Console.WriteLine("Account is inactive!");
            return;
        }

        if (amount <= 0)
        {
            Console.WriteLine("Deposit amount must be greater than 0!");
            return;
        }

        account.Balance += amount;
        account.Transactions.Add(new Transaction(++nextTransactionId, TransactionType.Deposit, amount, "Cash Deposit"));
        Console.WriteLine($"Deposit successful! New Balance: ${account.Balance:F2}");
    }

    public void Withdraw(string accountNumber, decimal amount)
    {
        var account = accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
        if (account == null)
        {
            Console.WriteLine("Account not found!");
            return;
        }

        if (!account.IsActive)
        {
            Console.WriteLine("Account is inactive!");
            return;
        }

        if (amount <= 0)
        {
            Console.WriteLine("Withdrawal amount must be greater than 0!");
            return;
        }

        if (account.Balance - amount < account.MinimumBalance)
        {
            if (!account.OverdraftProtection)
            {
                Console.WriteLine($"Insufficient funds! Minimum balance required: ${account.MinimumBalance:F2}");
                return;
            }
        }

        account.Balance -= amount;
        account.Transactions.Add(new Transaction(++nextTransactionId, TransactionType.Withdrawal, amount, "Cash Withdrawal"));
        Console.WriteLine($"Withdrawal successful! New Balance: ${account.Balance:F2}");
    }

    public void Transfer(string fromAccount, string toAccount, decimal amount)
    {
        var source = accounts.FirstOrDefault(a => a.AccountNumber == fromAccount);
        var destination = accounts.FirstOrDefault(a => a.AccountNumber == toAccount);

        if (source == null || destination == null)
        {
            Console.WriteLine("One or both accounts not found!");
            return;
        }

        if (!source.IsActive || !destination.IsActive)
        {
            Console.WriteLine("One or both accounts are inactive!");
            return;
        }

        if (amount <= 0)
        {
            Console.WriteLine("Transfer amount must be greater than 0!");
            return;
        }

        if (source.Balance - amount < source.MinimumBalance)
        {
            Console.WriteLine($"Insufficient funds! Minimum balance required: ${source.MinimumBalance:F2}");
            return;
        }

        source.Balance -= amount;
        destination.Balance += amount;

        source.Transactions.Add(new Transaction(++nextTransactionId, TransactionType.Transfer, amount, $"Transfer to {toAccount}", toAccount));
        destination.Transactions.Add(new Transaction(++nextTransactionId, TransactionType.Transfer, amount, $"Transfer from {fromAccount}", fromAccount));

        Console.WriteLine($"Transfer successful! From Account Balance: ${source.Balance:F2}");
    }

    public void CheckBalance(string accountNumber)
    {
        var account = accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
        if (account == null)
        {
            Console.WriteLine("Account not found!");
            return;
        }

        Console.WriteLine($"\n========== Account Balance ==========");
        Console.WriteLine($"Account Number: {account.AccountNumber}");
        Console.WriteLine($"Customer Name: {account.CustomerName}");
        Console.WriteLine($"Account Type: {account.AccountType}");
        Console.WriteLine($"Current Balance: ${account.Balance:F2}");
        Console.WriteLine($"Status: {(account.IsActive ? "Active" : "Inactive")}");
        Console.WriteLine("=====================================\n");
    }

    public void ViewTransactionHistory(string accountNumber, int recordCount = 10)
    {
        var account = accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
        if (account == null)
        {
            Console.WriteLine("Account not found!");
            return;
        }

        if (account.Transactions.Count == 0)
        {
            Console.WriteLine("No transactions found!");
            return;
        }

        Console.WriteLine($"\n========== Transaction History - {account.AccountNumber} ==========");
        var recentTransactions = account.Transactions.OrderByDescending(t => t.TransactionDate).Take(recordCount);
        foreach (var transaction in recentTransactions)
        {
            Console.WriteLine(transaction);
        }
        Console.WriteLine("===============================================\n");
    }

    public void ApplyInterest(string accountNumber)
    {
        var account = accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
        if (account == null)
        {
            Console.WriteLine("Account not found!");
            return;
        }

        if (!account.IsActive)
        {
            Console.WriteLine("Account is inactive!");
            return;
        }

        decimal interest = account.Balance * account.InterestRate / 12;
        account.Balance += interest;
        account.Transactions.Add(new Transaction(++nextTransactionId, TransactionType.Interest, interest, "Monthly Interest"));
        Console.WriteLine($"Interest applied! Amount: ${interest:F2}, New Balance: ${account.Balance:F2}");
    }

    public void ApplyMonthlyFee(string accountNumber)
    {
        var account = accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
        if (account == null)
        {
            Console.WriteLine("Account not found!");
            return;
        }

        if (!account.IsActive)
        {
            Console.WriteLine("Account is inactive!");
            return;
        }

        decimal fee = account.AccountType == AccountType.Checking ? 5 : account.AccountType == AccountType.Business ? 15 : 2;
        account.Balance -= fee;
        account.Transactions.Add(new Transaction(++nextTransactionId, TransactionType.Fee, fee, "Monthly Maintenance Fee"));
        Console.WriteLine($"Fee applied! Amount: ${fee:F2}, New Balance: ${account.Balance:F2}");
    }

    public void CloseAccount(string accountNumber)
    {
        var account = accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
        if (account == null)
        {
            Console.WriteLine("Account not found!");
            return;
        }

        if (!account.IsActive)
        {
            Console.WriteLine("Account is already closed!");
            return;
        }

        if (account.Balance != 0)
        {
            Console.WriteLine($"Cannot close account with balance ${account.Balance:F2}. Withdraw remaining balance first!");
            return;
        }

        account.IsActive = false;
        Console.WriteLine("Account closed successfully!");
    }

    public void ViewAllAccounts()
    {
        if (accounts.Count == 0)
        {
            Console.WriteLine("No accounts in the system!");
            return;
        }

        Console.WriteLine("\n========== All Accounts ==========");
        foreach (var account in accounts)
        {
            Console.WriteLine(account);
        }
        Console.WriteLine("==================================\n");
    }

    public void ViewAllCustomers()
    {
        if (customers.Count == 0)
        {
            Console.WriteLine("No customers in the system!");
            return;
        }

        Console.WriteLine("\n========== All Customers ==========");
        foreach (var customer in customers)
        {
            Console.WriteLine(customer);
        }
        Console.WriteLine("===================================\n");
    }

    public void ViewCustomerAccounts(int customerId)
    {
        var customer = customers.FirstOrDefault(c => c.CustomerId == customerId);
        if (customer == null)
        {
            Console.WriteLine("Customer not found!");
            return;
        }

        if (customer.Accounts.Count == 0)
        {
            Console.WriteLine("Customer has no accounts!");
            return;
        }

        Console.WriteLine($"\n========== Accounts for {customer.Name} ==========");
        foreach (var accountNumber in customer.Accounts)
        {
            var account = accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
            if (account != null)
            {
                Console.WriteLine(account);
            }
        }
        Console.WriteLine("================================================\n");
    }

    public void GenerateBankReport()
    {
        Console.WriteLine("\n========== Bank Report ==========");
        Console.WriteLine($"Total Customers: {customers.Count}");
        Console.WriteLine($"Total Accounts: {accounts.Count}");
        Console.WriteLine($"Active Accounts: {accounts.Count(a => a.IsActive)}");
        Console.WriteLine($"Inactive Accounts: {accounts.Count(a => !a.IsActive)}");
        Console.WriteLine($"Total Deposits: ${accounts.Sum(a => a.Balance):F2}");
        Console.WriteLine($"Total Transactions: {accounts.Sum(a => a.Transactions.Count)}");

        decimal savingsBalance = accounts.Where(a => a.AccountType == AccountType.Savings).Sum(a => a.Balance);
        decimal checkingBalance = accounts.Where(a => a.AccountType == AccountType.Checking).Sum(a => a.Balance);
        decimal businessBalance = accounts.Where(a => a.AccountType == AccountType.Business).Sum(a => a.Balance);

        Console.WriteLine($"\nAccount Type Breakdown:");
        Console.WriteLine($"  Savings: ${savingsBalance:F2}");
        Console.WriteLine($"  Checking: ${checkingBalance:F2}");
        Console.WriteLine($"  Business: ${businessBalance:F2}");
        Console.WriteLine("=================================\n");
    }
}

class Program
{
    static void Main()
    {
        BankingSystem bank = new BankingSystem();

        bank.RegisterCustomer("John Smith", "john.smith@email.com", "555-0101", "123 Main Street");
        bank.RegisterCustomer("Sarah Johnson", "sarah.j@email.com", "555-0102", "456 Oak Avenue");
        bank.RegisterCustomer("Michael Brown", "m.brown@email.com", "555-0103", "789 Pine Road");

        bank.CreateAccount(10001, AccountType.Savings, 5000);
        bank.CreateAccount(10001, AccountType.Checking, 2000);
        bank.CreateAccount(10002, AccountType.Savings, 10000);
        bank.CreateAccount(10003, AccountType.Business, 25000);

        bool running = true;
        while (running)
        {
            Console.WriteLine("\n================ Banking System ================");
            Console.WriteLine("1. Register Customer");
            Console.WriteLine("2. Create Account");
            Console.WriteLine("3. Deposit");
            Console.WriteLine("4. Withdraw");
            Console.WriteLine("5. Transfer");
            Console.WriteLine("6. Check Balance");
            Console.WriteLine("7. View Transaction History");
            Console.WriteLine("8. Apply Interest");
            Console.WriteLine("9. Apply Monthly Fee");
            Console.WriteLine("10. Close Account");
            Console.WriteLine("11. View All Accounts");
            Console.WriteLine("12. View All Customers");
            Console.WriteLine("13. View Customer Accounts");
            Console.WriteLine("14. Generate Bank Report");
            Console.WriteLine("15. Exit");
            Console.WriteLine("==============================================");
            Console.Write("Enter your choice (1-15): ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        Console.Write("Enter Name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter Email: ");
                        string email = Console.ReadLine();
                        Console.Write("Enter Phone Number: ");
                        string phone = Console.ReadLine();
                        Console.Write("Enter Address: ");
                        string address = Console.ReadLine();
                        bank.RegisterCustomer(name, email, phone, address);
                        break;

                    case 2:
                        Console.Write("Enter Customer ID: ");
                        if (int.TryParse(Console.ReadLine(), out int custId))
                        {
                            Console.WriteLine("Account Types: 0=Savings, 1=Checking, 2=Business");
                            Console.Write("Enter Account Type (0-2): ");
                            if (int.TryParse(Console.ReadLine(), out int accType) && accType >= 0 && accType <= 2)
                            {
                                Console.Write("Enter Initial Deposit: ");
                                if (decimal.TryParse(Console.ReadLine(), out decimal deposit) && deposit > 0)
                                {
                                    bank.CreateAccount(custId, (AccountType)accType, deposit);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid deposit amount!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid account type!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Customer ID!");
                        }
                        break;

                    case 3:
                        Console.Write("Enter Account Number: ");
                        string depositAccount = Console.ReadLine();
                        Console.Write("Enter Deposit Amount: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal depositAmount) && depositAmount > 0)
                        {
                            bank.Deposit(depositAccount, depositAmount);
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount!");
                        }
                        break;

                    case 4:
                        Console.Write("Enter Account Number: ");
                        string withdrawAccount = Console.ReadLine();
                        Console.Write("Enter Withdrawal Amount: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal withdrawAmount) && withdrawAmount > 0)
                        {
                            bank.Withdraw(withdrawAccount, withdrawAmount);
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount!");
                        }
                        break;

                    case 5:
                        Console.Write("Enter From Account Number: ");
                        string fromAcc = Console.ReadLine();
                        Console.Write("Enter To Account Number: ");
                        string toAcc = Console.ReadLine();
                        Console.Write("Enter Transfer Amount: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal transferAmount) && transferAmount > 0)
                        {
                            bank.Transfer(fromAcc, toAcc, transferAmount);
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount!");
                        }
                        break;

                    case 6:
                        Console.Write("Enter Account Number: ");
                        string balanceAccount = Console.ReadLine();
                        bank.CheckBalance(balanceAccount);
                        break;

                    case 7:
                        Console.Write("Enter Account Number: ");
                        string historyAccount = Console.ReadLine();
                        Console.Write("Enter Number of Records (default 10): ");
                        if (int.TryParse(Console.ReadLine(), out int records) && records > 0)
                        {
                            bank.ViewTransactionHistory(historyAccount, records);
                        }
                        else
                        {
                            bank.ViewTransactionHistory(historyAccount);
                        }
                        break;

                    case 8:
                        Console.Write("Enter Account Number: ");
                        string interestAccount = Console.ReadLine();
                        bank.ApplyInterest(interestAccount);
                        break;

                    case 9:
                        Console.Write("Enter Account Number: ");
                        string feeAccount = Console.ReadLine();
                        bank.ApplyMonthlyFee(feeAccount);
                        break;

                    case 10:
                        Console.Write("Enter Account Number: ");
                        string closeAccount = Console.ReadLine();
                        bank.CloseAccount(closeAccount);
                        break;

                    case 11:
                        bank.ViewAllAccounts();
                        break;

                    case 12:
                        bank.ViewAllCustomers();
                        break;

                    case 13:
                        Console.Write("Enter Customer ID: ");
                        if (int.TryParse(Console.ReadLine(), out int viewCustId))
                        {
                            bank.ViewCustomerAccounts(viewCustId);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Customer ID!");
                        }
                        break;

                    case 14:
                        bank.GenerateBankReport();
                        break;

                    case 15:
                        running = false;
                        Console.WriteLine("Thank you for using Banking System!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice! Please enter a number between 1 and 15.");
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
