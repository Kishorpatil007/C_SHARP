using System;

static class MyExtension
{
    public static int Cube(this int x)
    {
        return x * x * x;
    }
}

class Program
{
    static void Main()
    {
        int num = 3;
        Console.WriteLine(num.Cube());
    }
}