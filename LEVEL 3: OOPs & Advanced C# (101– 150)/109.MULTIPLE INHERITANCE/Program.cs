using System;

interface IA
{
    void showA();
}

interface IB
{
    void showB();
}

class Demo : IA, IB
{
    public void showA()
    {
        Console.WriteLine("Interface A");
    }

    public void showB()
    {
        Console.WriteLine("Interface B");
    }
}

class Program
{
    static void Main()
    {
        Demo d = new Demo();
        d.showA();
        d.showB();
    }
}