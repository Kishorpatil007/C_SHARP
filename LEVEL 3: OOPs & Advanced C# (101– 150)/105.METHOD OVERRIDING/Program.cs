using System;

class Parent
{
    public virtual void show()
    {
        Console.WriteLine("Parent Method");
    }
}

class Child : Parent
{
    public override void show()
    {
        Console.WriteLine("Child Method");
    }
}

class Program
{
    static void Main()
    {
        Parent p = new Child();
        p.show();
    }
}