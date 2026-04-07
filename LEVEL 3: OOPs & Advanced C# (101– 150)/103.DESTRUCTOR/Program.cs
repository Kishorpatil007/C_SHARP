using System;

class Demo
{
    public Demo()
    {
        Console.WriteLine("Constructor called");
    }

    ~Demo()
    {
        Console.WriteLine("Destructor called");
    }
}

class Program
{
    static void Main()
    {
        Demo d = new Demo();
    }
}