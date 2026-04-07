using System;

class Demo
{
    public void add(int a, int b)
    {
        Console.WriteLine(a + b);
    }

    public void add(double a, double b)
    {
        Console.WriteLine(a + b);
    }
}

class Program
{
    static void Main()
    {
        Demo d = new Demo();
        d.add(10, 20);
        d.add(5.5, 2.5);
    }
}