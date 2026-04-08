using System;

static class Demo
{
    public static int a = 10;

    public static void Show()
    {
        Console.WriteLine(a);
    }
}

class Program
{
    static void Main()
    {
        Demo.Show();
    }
}