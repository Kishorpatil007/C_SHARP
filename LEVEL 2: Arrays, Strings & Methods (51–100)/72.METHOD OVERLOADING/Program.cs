using System;

class Program
{
    static int Add(int a, int b)
    {
        return a + b;
    }

    static double Add(double a, double b)
    {
        return a + b;
    }

    static void Main()
    {
        Console.WriteLine(Add(2, 3));
        Console.WriteLine(Add(2.5, 3.5));
    }
}