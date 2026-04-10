using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<int> numbers = new List<int>();
        numbers.Add(10);
        numbers.Add(20);
        numbers.Add(30);

        Console.WriteLine("List elements:");
        foreach(int num in numbers)
        {
            Console.WriteLine(num);
        }

        numbers.Remove(20);
        Console.WriteLine("After removing 20:");
        foreach(int num in numbers)
        {
            Console.WriteLine(num);
        }
    }
}