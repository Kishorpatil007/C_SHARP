using System;

class DateTimeDemo
{
    static void Main()
    {
        DateTime now = DateTime.Now;

        Console.WriteLine("Current Date: " + now.ToShortDateString());
        Console.WriteLine("Current Time: " + now.ToShortTimeString());
        Console.WriteLine("Day: " + now.Day);
        Console.WriteLine("Month: " + now.Month);
        Console.WriteLine("Year: " + now.Year);
    }
}