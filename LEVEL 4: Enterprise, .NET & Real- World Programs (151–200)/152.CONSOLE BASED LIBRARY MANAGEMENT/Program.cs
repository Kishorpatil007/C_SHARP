using System;
using System.Collections.Generic;
using System.Linq;

class Book
{
    public string ISBN { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Category { get; set; }
    public int TotalCopies { get; set; }
    public int AvailableCopies { get; set; }
    public decimal Price { get; set; }
    public DateTime PublishedDate { get; set; }

    public Book(string isbn, string title, string author, string category, int totalCopies, decimal price, DateTime publishedDate)
    {
        ISBN = isbn;
        Title = title;
        Author = author;
        Category = category;
        TotalCopies = totalCopies;
        AvailableCopies = totalCopies;
        Price = price;
        PublishedDate = publishedDate;
    }

    public override string ToString()
    {
        return $"ISBN: {ISBN} | Title: {Title} | Author: {Author} | Category: {Category} | Available: {AvailableCopies}/{TotalCopies} | Price: ${Price}";
    }
}

class Member
{
    public int MemberId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime RegistrationDate { get; set; }
    public bool IsActive { get; set; }
    public int BooksIssued { get; set; }

    public Member(int memberId, string name, string email, string phoneNumber)
    {
        MemberId = memberId;
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        RegistrationDate = DateTime.Now;
        IsActive = true;
        BooksIssued = 0;
    }

    public override string ToString()
    {
        return $"ID: {MemberId} | Name: {Name} | Email: {Email} | Phone: {PhoneNumber} | Books Issued: {BooksIssued} | Active: {IsActive}";
    }
}

class Transaction
{
    public int TransactionId { get; set; }
    public int MemberId { get; set; }
    public string ISBN { get; set; }
    public DateTime IssuedDate { get; set; }
    public DateTime? ReturnedDate { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsReturned { get; set; }
    public decimal Fine { get; set; }

    public Transaction(int transactionId, int memberId, string isbn, int borrowDays = 14)
    {
        TransactionId = transactionId;
        MemberId = memberId;
        ISBN = isbn;
        IssuedDate = DateTime.Now;
        DueDate = DateTime.Now.AddDays(borrowDays);
        ReturnedDate = null;
        IsReturned = false;
        Fine = 0;
    }

    public override string ToString()
    {
        string returnInfo = IsReturned ? $"Returned: {ReturnedDate:yyyy-MM-dd}" : "Not Returned";
        return $"ID: {TransactionId} | Member: {MemberId} | ISBN: {ISBN} | Issued: {IssuedDate:yyyy-MM-dd} | Due: {DueDate:yyyy-MM-dd} | {returnInfo} | Fine: ${Fine}";
    }
}

class LibraryManagementSystem
{
    private List<Book> books = new List<Book>();
    private List<Member> members = new List<Member>();
    private List<Transaction> transactions = new List<Transaction>();
    private int nextMemberId = 1001;
    private int nextTransactionId = 1;

    public void AddBook(string isbn, string title, string author, string category, int copies, decimal price, DateTime publishedDate)
    {
        if (books.Any(b => b.ISBN == isbn))
        {
            Console.WriteLine("Book with this ISBN already exists!");
            return;
        }
        books.Add(new Book(isbn, title, author, category, copies, price, publishedDate));
        Console.WriteLine($"Book '{title}' added successfully!");
    }

    public void RemoveBook(string isbn)
    {
        var book = books.FirstOrDefault(b => b.ISBN == isbn);
        if (book == null)
        {
            Console.WriteLine("Book not found!");
            return;
        }
        if (book.TotalCopies != book.AvailableCopies)
        {
            Console.WriteLine("Cannot remove book. Some copies are issued!");
            return;
        }
        books.Remove(book);
        Console.WriteLine("Book removed successfully!");
    }

    public void RegisterMember(string name, string email, string phoneNumber)
    {
        var member = new Member(nextMemberId++, name, email, phoneNumber);
        members.Add(member);
        Console.WriteLine($"Member registered successfully! Member ID: {member.MemberId}");
    }

    public void IssueBook(int memberId, string isbn)
    {
        var member = members.FirstOrDefault(m => m.MemberId == memberId);
        if (member == null)
        {
            Console.WriteLine("Member not found!");
            return;
        }

        if (!member.IsActive)
        {
            Console.WriteLine("Member account is inactive!");
            return;
        }

        var book = books.FirstOrDefault(b => b.ISBN == isbn);
        if (book == null)
        {
            Console.WriteLine("Book not found!");
            return;
        }

        if (book.AvailableCopies <= 0)
        {
            Console.WriteLine("No copies available!");
            return;
        }

        var pendingTransaction = transactions.FirstOrDefault(t => t.MemberId == memberId && t.ISBN == isbn && !t.IsReturned);
        if (pendingTransaction != null)
        {
            Console.WriteLine("Member already has this book issued!");
            return;
        }

        book.AvailableCopies--;
        member.BooksIssued++;
        var transaction = new Transaction(nextTransactionId++, memberId, isbn);
        transactions.Add(transaction);
        Console.WriteLine($"Book issued successfully! Transaction ID: {transaction.TransactionId}");
    }

    public void ReturnBook(int transactionId)
    {
        var transaction = transactions.FirstOrDefault(t => t.TransactionId == transactionId);
        if (transaction == null)
        {
            Console.WriteLine("Transaction not found!");
            return;
        }

        if (transaction.IsReturned)
        {
            Console.WriteLine("Book already returned!");
            return;
        }

        var book = books.FirstOrDefault(b => b.ISBN == transaction.ISBN);
        if (book != null)
        {
            book.AvailableCopies++;
        }

        var member = members.FirstOrDefault(m => m.MemberId == transaction.MemberId);
        if (member != null)
        {
            member.BooksIssued--;
        }

        transaction.ReturnedDate = DateTime.Now;
        transaction.IsReturned = true;

        if (DateTime.Now > transaction.DueDate)
        {
            int daysLate = (int)(DateTime.Now - transaction.DueDate).TotalDays;
            transaction.Fine = daysLate * 5;
            Console.WriteLine($"Book returned! Days late: {daysLate}, Fine: ${transaction.Fine}");
        }
        else
        {
            Console.WriteLine("Book returned on time!");
        }
    }

    public void SearchBooks(string searchTerm)
    {
        var searchResults = books.Where(b =>
            b.Title.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
            b.Author.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
            b.Category.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
            b.ISBN.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0
        ).ToList();

        if (searchResults.Count == 0)
        {
            Console.WriteLine("No books found!");
            return;
        }

        Console.WriteLine("\n========== Search Results ==========");
        foreach (var book in searchResults)
        {
            Console.WriteLine(book);
        }
        Console.WriteLine("====================================\n");
    }

    public void ViewAllBooks()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("No books in the library!");
            return;
        }

        Console.WriteLine("\n========== All Books ==========");
        foreach (var book in books)
        {
            Console.WriteLine(book);
        }
        Console.WriteLine("===============================\n");
    }

    public void ViewAllMembers()
    {
        if (members.Count == 0)
        {
            Console.WriteLine("No members registered!");
            return;
        }

        Console.WriteLine("\n========== All Members ==========");
        foreach (var member in members)
        {
            Console.WriteLine(member);
        }
        Console.WriteLine("==================================\n");
    }

    public void ViewAllTransactions()
    {
        if (transactions.Count == 0)
        {
            Console.WriteLine("No transactions!");
            return;
        }

        Console.WriteLine("\n========== All Transactions ==========");
        foreach (var transaction in transactions)
        {
            Console.WriteLine(transaction);
        }
        Console.WriteLine("=======================================\n");
    }

    public void ViewMemberTransactions(int memberId)
    {
        var memberTransactions = transactions.Where(t => t.MemberId == memberId).ToList();
        if (memberTransactions.Count == 0)
        {
            Console.WriteLine("No transactions for this member!");
            return;
        }

        Console.WriteLine($"\n========== Transactions for Member {memberId} ==========");
        foreach (var transaction in memberTransactions)
        {
            Console.WriteLine(transaction);
        }
        Console.WriteLine("========================================================\n");
    }

    public void ViewPendingReturns()
    {
        var pendingReturns = transactions.Where(t => !t.IsReturned).ToList();
        if (pendingReturns.Count == 0)
        {
            Console.WriteLine("No pending returns!");
            return;
        }

        Console.WriteLine("\n========== Pending Returns ==========");
        foreach (var transaction in pendingReturns)
        {
            Console.WriteLine(transaction);
        }
        Console.WriteLine("=====================================\n");
    }

    public void GenerateReport()
    {
        Console.WriteLine("\n========== Library Report ==========");
        Console.WriteLine($"Total Books: {books.Count}");
        Console.WriteLine($"Total Books Available: {books.Sum(b => b.AvailableCopies)}");
        Console.WriteLine($"Total Books Issued: {books.Sum(b => b.TotalCopies - b.AvailableCopies)}");
        Console.WriteLine($"Total Members: {members.Count}");
        Console.WriteLine($"Active Members: {members.Count(m => m.IsActive)}");
        Console.WriteLine($"Total Transactions: {transactions.Count}");
        Console.WriteLine($"Completed Transactions: {transactions.Count(t => t.IsReturned)}");
        Console.WriteLine($"Pending Transactions: {transactions.Count(t => !t.IsReturned)}");
        Console.WriteLine($"Total Fine Collected: ${transactions.Sum(t => t.Fine)}");
        Console.WriteLine("====================================\n");
    }
}

class Program
{
    static void Main()
    {
        LibraryManagementSystem library = new LibraryManagementSystem();
        
        library.AddBook("ISBN001", "C# Programming", "John Smith", "Programming", 5, 49.99m, new DateTime(2020, 1, 15));
        library.AddBook("ISBN002", "The Great Gatsby", "F. Scott Fitzgerald", "Fiction", 3, 12.99m, new DateTime(1925, 4, 10));
        library.AddBook("ISBN003", "To Kill a Mockingbird", "Harper Lee", "Fiction", 4, 14.99m, new DateTime(1960, 7, 11));
        library.AddBook("ISBN004", "Clean Code", "Robert C. Martin", "Programming", 6, 45.00m, new DateTime(2008, 8, 1));
        library.AddBook("ISBN005", "1984", "George Orwell", "Fiction", 3, 13.99m, new DateTime(1949, 6, 8));

        bool running = true;
        while (running)
        {
            Console.WriteLine("\n================ Library Management System ================");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Remove Book");
            Console.WriteLine("3. Register Member");
            Console.WriteLine("4. Issue Book");
            Console.WriteLine("5. Return Book");
            Console.WriteLine("6. Search Book");
            Console.WriteLine("7. View All Books");
            Console.WriteLine("8. View All Members");
            Console.WriteLine("9. View All Transactions");
            Console.WriteLine("10. View Member Transactions");
            Console.WriteLine("11. View Pending Returns");
            Console.WriteLine("12. Generate Report");
            Console.WriteLine("13. Exit");
            Console.WriteLine("===========================================================");
            Console.Write("Enter your choice (1-13): ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        Console.Write("Enter ISBN: ");
                        string isbn = Console.ReadLine();
                        Console.Write("Enter Title: ");
                        string title = Console.ReadLine();
                        Console.Write("Enter Author: ");
                        string author = Console.ReadLine();
                        Console.Write("Enter Category: ");
                        string category = Console.ReadLine();
                        Console.Write("Enter Number of Copies: ");
                        if (int.TryParse(Console.ReadLine(), out int copies) && copies > 0)
                        {
                            Console.Write("Enter Price: ");
                            if (decimal.TryParse(Console.ReadLine(), out decimal price) && price > 0)
                            {
                                Console.Write("Enter Published Date (yyyy-MM-dd): ");
                                if (DateTime.TryParse(Console.ReadLine(), out DateTime publishedDate))
                                {
                                    library.AddBook(isbn, title, author, category, copies, price, publishedDate);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid date format!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid price!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid number of copies!");
                        }
                        break;

                    case 2:
                        Console.Write("Enter ISBN of book to remove: ");
                        string removeIsbn = Console.ReadLine();
                        library.RemoveBook(removeIsbn);
                        break;

                    case 3:
                        Console.Write("Enter Member Name: ");
                        string memberName = Console.ReadLine();
                        Console.Write("Enter Email: ");
                        string email = Console.ReadLine();
                        Console.Write("Enter Phone Number: ");
                        string phone = Console.ReadLine();
                        library.RegisterMember(memberName, email, phone);
                        break;

                    case 4:
                        Console.Write("Enter Member ID: ");
                        if (int.TryParse(Console.ReadLine(), out int memberId))
                        {
                            Console.Write("Enter Book ISBN: ");
                            string issueIsbn = Console.ReadLine();
                            library.IssueBook(memberId, issueIsbn);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Member ID!");
                        }
                        break;

                    case 5:
                        Console.Write("Enter Transaction ID to return: ");
                        if (int.TryParse(Console.ReadLine(), out int transactionId))
                        {
                            library.ReturnBook(transactionId);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Transaction ID!");
                        }
                        break;

                    case 6:
                        Console.Write("Enter search term (Title/Author/Category/ISBN): ");
                        string searchTerm = Console.ReadLine();
                        library.SearchBooks(searchTerm);
                        break;

                    case 7:
                        library.ViewAllBooks();
                        break;

                    case 8:
                        library.ViewAllMembers();
                        break;

                    case 9:
                        library.ViewAllTransactions();
                        break;

                    case 10:
                        Console.Write("Enter Member ID: ");
                        if (int.TryParse(Console.ReadLine(), out int memId))
                        {
                            library.ViewMemberTransactions(memId);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Member ID!");
                        }
                        break;

                    case 11:
                        library.ViewPendingReturns();
                        break;

                    case 12:
                        library.GenerateReport();
                        break;

                    case 13:
                        running = false;
                        Console.WriteLine("Thank you for using Library Management System!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice! Please enter a number between 1 and 13.");
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
