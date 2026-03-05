using System;

class Program
{
    const double pi = 3.14;
    readonly int number;

    public Program(int n)
    {
        number = n;
    }

    static void Main()
    {
        Program obj = new Program(100);
        Console.WriteLine("Constant PI: " + pi);
        Console.WriteLine("Readonly Number: " + obj.number);
    }
}
