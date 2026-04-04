using System;

class Program
{
    static void Show(string name = "Guest")
    {
        Console.WriteLine("Hello " + name);
    }

    static void Main()
    {
        Show();
        Show("Kishor");
    }
}