using System;

partial class Demo
{
    public void Show1()
    {
        Console.WriteLine("Method 1");
    }
}

partial class Demo
{
    public void Show2()
    {
        Console.WriteLine("Method 2");
    }
}

class Program
{
    static void Main()
    {
        Demo d = new Demo();
        d.Show1();
        d.Show2();
    }
}