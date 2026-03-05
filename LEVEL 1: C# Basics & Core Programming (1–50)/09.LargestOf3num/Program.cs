using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter first number: ");
        int a = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter second number: ");
        int b = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter third number: ");
        int c = Convert.ToInt32(Console.ReadLine());

        int max = a;

        if (b > max)
            max = b;
        if (c > max)
            max = c;

        Console.WriteLine("Largest: " + max);
    }
}