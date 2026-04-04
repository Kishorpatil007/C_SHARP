using System;

class Program
{
    static void PrintNumbers(int n)
    {
        if (n == 0) return;
        Console.Write(n + " ");
        PrintNumbers(n - 1);
    }

    static void Main()
    {
        Console.Write("Enter number: ");
        int n = int.Parse(Console.ReadLine());
        PrintNumbers(n);
    }
}