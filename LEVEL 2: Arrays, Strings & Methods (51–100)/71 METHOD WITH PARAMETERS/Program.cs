using System;

class Program
{
    static void Add(int a, int b)
    {
        Console.WriteLine(a + b);
    }

    static void Main()
    {
        int x = int.Parse(Console.ReadLine());
        int y = int.Parse(Console.ReadLine());
        Add(x, y);
    }
}