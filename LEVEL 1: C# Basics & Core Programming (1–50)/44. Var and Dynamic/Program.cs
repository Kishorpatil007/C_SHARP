using System;

class VarDynamicDemo
{
    static void Main()
    {
        var a = 10;
        var b = "Hello";

        Console.WriteLine(a);
        Console.WriteLine(b);

        dynamic x = 20;
        Console.WriteLine(x);

        x = "World";
        Console.WriteLine(x);
    }
}