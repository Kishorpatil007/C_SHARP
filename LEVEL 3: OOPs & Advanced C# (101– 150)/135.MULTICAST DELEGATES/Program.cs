using System;

delegate void Show();

class Program
{
    static void Method1()
    {
        Console.WriteLine("Method1");
    }

    static void Method2()
    {
        Console.WriteLine("Method2");
    }

    static void Main()
    {
        Show s = Method1;
        s += Method2;

        s();
    }
}