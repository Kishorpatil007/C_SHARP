using System;

delegate int Operation(int a, int b);

class Program
{
    static int Add(int a, int b)
    {
        return a + b;
    }

    static void Main()
    {
        Operation op = Add;

        Console.WriteLine(op(10, 5));
    }
}