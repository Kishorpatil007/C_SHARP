using System;
using System.Linq;

class Program
{
    static void Main()
    {
        int[] numbers = { 1, 2, 3, 4, 5, 6 };

        var evenNumbers = numbers.Where(n => n % 2 == 0);

        var squares = evenNumbers.Select(n => n * n);

        foreach (var num in squares)
        {
            Console.WriteLine(num);
        }
    }
}