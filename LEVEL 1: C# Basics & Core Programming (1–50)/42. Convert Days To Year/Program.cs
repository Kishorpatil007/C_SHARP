using System;

class DaysConvert
{
    static void Main()
    {
        Console.Write("Enter number of days: ");
        int days = int.Parse(Console.ReadLine());

        int years = days / 365;
        days = days % 365;
        int months = days / 30;
        days = days % 30;

        Console.WriteLine("Years: " + years);
        Console.WriteLine("Months: " + months);
        Console.WriteLine("Days: " + days);
    }
}