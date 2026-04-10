using System;
using System.Collections;

class Program
{
    static void Main()
    {
        ArrayList list = new ArrayList();
        list.Add(10);
        list.Add("Hello");
        list.Add(25.5);
        list.Add(true);

        Console.WriteLine("ArrayList elements:");
        foreach(var item in list)
        {
            Console.WriteLine(item);
        }

        list.Remove("Hello");
        Console.WriteLine("After removing 'Hello':");
        foreach(var item in list)
        {
            Console.WriteLine(item);
        }
    }
}