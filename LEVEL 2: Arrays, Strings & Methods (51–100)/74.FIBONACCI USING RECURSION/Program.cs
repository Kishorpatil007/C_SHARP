using System;

class Program
{
    static int Fib(int n)
    {
        if (n <= 1) return n;
        return Fib(n - 1) + Fib(n - 2);
    }

    static void Main()
    {
        Console.Write("Enter limit: ");
        int n = int.Parse(Console.ReadLine());
        for (int i = 0; i < n; i++)
            Console.Write(Fib(i) + " ");
    }
}