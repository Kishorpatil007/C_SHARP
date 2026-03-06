using System;

class NullableDemo
{
    static void Main()
    {
        int? num = null;

        if (num.HasValue)
            Console.WriteLine("Value: " + num.Value);
        else
            Console.WriteLine("Value is Null");

        num = 50;

        if (num.HasValue)
            Console.WriteLine("Value: " + num.Value);
        else
            Console.WriteLine("Value is Null");
    }
}