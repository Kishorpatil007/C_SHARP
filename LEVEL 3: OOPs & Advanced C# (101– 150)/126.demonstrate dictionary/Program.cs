using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Dictionary<int, string> students = new Dictionary<int, string>();

        students.Add(1, "Amit");
        students.Add(2, "Rahul");
        students.Add(3, "Sneha");

        foreach (var item in students)
        {
            Console.WriteLine(item.Key + " " + item.Value);
        }

        Console.WriteLine("Enter key to search:");
        int key = int.Parse(Console.ReadLine());

        if (students.ContainsKey(key))
        {
            Console.WriteLine(students[key]);
        }
        else
        {
            Console.WriteLine("Not Found");
        }
    }
}