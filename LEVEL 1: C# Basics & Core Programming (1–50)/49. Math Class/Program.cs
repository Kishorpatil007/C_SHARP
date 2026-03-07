using System;

class MathDemo
{
    static void Main()
    {
        double a = 25;
        double b = 4;

        Console.WriteLine("Square Root: " + Math.Sqrt(a));
        Console.WriteLine("Power: " + Math.Pow(a, 2));
        Console.WriteLine("Maximum: " + Math.Max(a, b));
        Console.WriteLine("Minimum: " + Math.Min(a, b));
    }
}