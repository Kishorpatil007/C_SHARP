using System;

class Program
{
    delegate void Show(int x);

    static void Main()
    {
        Show s = delegate (int x)
        {
            Console.WriteLine(x * x);
        };

        s(5);
    }
}