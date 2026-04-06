using System;

class Program
{
    static void Main()
    {
        int[] numbers = { 10, 20, 30, 40, 50 };

        Span<int> span = numbers;
        span[1] = 99;

        ReadOnlySpan<int> readOnlySpan = numbers;

        foreach (var n in span)
        {
            Console.Write(n + " ");
        }

        Console.WriteLine();

        foreach (var n in readOnlySpan)
        {
            Console.Write(n + " ");
        }
    }
}