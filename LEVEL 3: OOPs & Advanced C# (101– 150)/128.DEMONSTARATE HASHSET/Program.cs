using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        HashSet<int> numbers = new HashSet<int>();

        numbers.Add(10);
        numbers.Add(20);
        numbers.Add(30);
        numbers.Add(20);

        foreach (int n in numbers)
        {
            Console.WriteLine(n);
        }

        Console.WriteLine("Enter number to check:");
        int x = int.Parse(Console.ReadLine());

        if (numbers.Contains(x))
        {
            Console.WriteLine("Exists");
        }
        else
        {
            Console.WriteLine("Does not exist");
        }
    }
}