using System;

class Demo
{
    public int value;
}

class Program
{
    static void Main()
    {
        int a = 10;
        int b = a;
        b = 20;

        Console.WriteLine("Value Types:");
        Console.WriteLine(a);
        Console.WriteLine(b);

        Demo obj1 = new Demo();
        obj1.value = 10;

        Demo obj2 = obj1;
        obj2.value = 20;

        Console.WriteLine("Reference Types:");
        Console.WriteLine(obj1.value);
        Console.WriteLine(obj2.value);
    }
}