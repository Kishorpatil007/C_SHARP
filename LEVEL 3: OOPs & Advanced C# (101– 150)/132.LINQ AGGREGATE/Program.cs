using System;
using System.Linq;

class Program
{
    static void Main()
    {
        int[] numbers = { 1, 2, 3, 4, 5 };

        int sum = numbers.Aggregate((a, b) => a + b);

        Console.WriteLine(sum);
    }
}