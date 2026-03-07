using System;

class EnumDemo
{
    enum Days { Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday }

    static void Main()
    {
        Console.Write("Enter number (0-6): ");
        int n = int.Parse(Console.ReadLine());

        Days day = (Days)n;
        Console.WriteLine("Day is: " + day);
    }
}