using System;

interface IMessage
{
    void SendMessage(string message);
}

class EmailMessage : IMessage
{
    public void SendMessage(string message)
    {
        Console.WriteLine("Email sent: " + message);
    }
}

class Notification
{
    private IMessage _messageService;

    public Notification(IMessage messageService)
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
    static void Main()
    {
        IMessage email = new EmailMessage();
        Notification notification = new Notification(email);
        notification.Notify("Hello, Kishor!");
    }
}