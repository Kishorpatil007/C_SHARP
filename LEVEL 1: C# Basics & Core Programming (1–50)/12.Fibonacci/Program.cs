using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter number of terms: ");
        int n = Convert.ToInt32(Console.ReadLine());

        int first = 0, second = 1, next;

        Console.WriteLine("Fibonacci Series:");

        for (int i = 1; i <= n; i++)
        {
            Console.Write(first + " ");

            next = first + second;
            first = second;
            second = next;
        }
    }
}