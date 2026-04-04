using System;

class Program
{
    static int Fact(int n)
    {
        if (n == 0) return 1;
        return n * Fact(n - 1);
    }

    static void Main()
    {
        Console.Write("Enter number: ");
        int n = int.Parse(Console.ReadLine());
        Console.WriteLine(Fact(n));
    }
}