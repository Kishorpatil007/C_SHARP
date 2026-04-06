using System;

class Demo
{
    public Demo()
    {
        Console.WriteLine("Object Created");
    }

    ~Demo()
    {
        Console.WriteLine("Object Destroyed");
    }
}

class Program
{
    static void Main()
    {
        Demo d1 = new Demo();
        Demo d2 = new Demo();

        d1 = null;
        GC.Collect();
        GC.WaitForPendingFinalizers();

        Console.WriteLine("End of Main");
    }
}