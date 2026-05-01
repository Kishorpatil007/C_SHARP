using System;

// Service Interface
public interface IMessageService
{
    void SendMessage(string message);
}

// Service Implementation
public class EmailService : IMessageService
{
    public void SendMessage(string message)
    {
        Console.WriteLine("Email Sent: " + message);
    }
}

// Client Class
public class Notification
{
    private readonly IMessageService _messageService;

    // Constructor Injection
    public Notification(IMessageService messageService)
    {
        _messageService = messageService;
    }

    public void Notify(string message)
    {
        _messageService.SendMessage(message);
    }
}

class Program
{
    static void Main(string[] args)
    {
        IMessageService service = new EmailService(); // Inject dependency
        Notification notification = new Notification(service);

        notification.Notify("Hello Dependency Injection!");
    }
}