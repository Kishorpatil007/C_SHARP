using System;

class Demo
{
    public int a = 10;
    private int b = 20;
    protected int c = 30;

    public void Show()
    {
        Console.WriteLine(a);
        Console.WriteLine(b);
        Console.WriteLine(c);
    }
}

class Test : Demo
{
    public void Display()
    {
        Console.WriteLine(a);
        Console.WriteLine(c);
    }
}

class Program
{
    static void Main()
    {
        Demo d = new Demo();
        d.Show();

        Test t = new Test();
        t.Display();
    }
}