using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter an integer: ");
        int a = Convert.ToInt32(Console.ReadLine());

        double b = a;
        Console.WriteLine("Implicit Casting to double: " + b);

        double x = 45.67;
        int y = (int)x;
        Console.WriteLine("Explicit Casting to int: " + y);
    }
}