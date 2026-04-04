using System;

class Program
{
    static void UsingOut(out int x)
    {
        x = 50;
    }

    static void UsingRef(ref int y)
    {
        y += 10;
    }

    static void Main()
    {
        int a;
        UsingOut(out a);
        Console.WriteLine(a);

        int b = 20;
        UsingRef(ref b);
        Console.WriteLine(b);
    }
}