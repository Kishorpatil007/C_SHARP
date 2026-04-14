using System;

class Program
{
    static void Main()
    {
        Func<int, int, int> add = (a, b) => a + b;
        Console.WriteLine(add(2, 3));

        Action<string> print = s => Console.WriteLine(s);
        print("Hello");

        Predicate<int> isEven = x => x % 2 == 0;
        Console.WriteLine(isEven(4));
    }
}