using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter a number (1-3): ");
        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            case 1:
                Console.WriteLine("Monday");
                break;
            case 2:
                Console.WriteLine("Tuesday");
                break;
            case 3:
                Console.WriteLine("Wednesday");
                break;
            default:
                Console.WriteLine("Invalid Choice");
                break;
        }
    }
}