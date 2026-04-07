using System;

class Parent
{
    public void show()
    {
        Console.WriteLine("Parent Class");
    }
}

class Child : Parent
{
    public void display()
    {
        Console.WriteLine("Child Class");
    }
}

class Program
{
    static void Main()
    {
        Child c = new Child();
        c.show();
        c.display();
    }
}