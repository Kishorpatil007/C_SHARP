using System;

class Program
{
    static void Main()
    {
        Func<int, int, int> add = (a, b) => a + b;

        Console.WriteLine(add(5, 3));

        Func<int, int> square = x => x * x;

        Console.WriteLine(square(4));
    }
}