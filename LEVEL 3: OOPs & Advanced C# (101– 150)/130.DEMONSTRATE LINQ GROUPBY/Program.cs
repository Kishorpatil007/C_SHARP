using System;
using System.Linq;

class Program
{
    static void Main()
    {
        string[] names = { "Amit", "Anil", "Rahul", "Ravi", "Sneha", "Sita" };

        var grouped = names.GroupBy(n => n[0]);

        foreach (var group in grouped)
        {
            Console.WriteLine(group.Key);

            foreach (var name in group)
            {
                Console.WriteLine(name);
            }
        }
    }
}