using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter Principal: ");
        int p = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter Rate: ");
        int r = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter Time: ");
        int t = Convert.ToInt32(Console.ReadLine());

        int si = (p * r * t) / 100;

        Console.WriteLine("Simple Interest: " + si);
    }
}