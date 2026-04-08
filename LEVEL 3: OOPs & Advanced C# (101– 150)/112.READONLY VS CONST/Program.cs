using System;

class Demo
{
    public readonly int x;
    public const int y = 50;

    public Demo(int val)
    {
        x = val;
    }

    public void Show()
    {
        Console.WriteLine(x);
        Console.WriteLine(y);
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Enter value: ");
        int val = int.Parse(Console.ReadLine());

        Demo d = new Demo(val);
        d.Show();
    }
}