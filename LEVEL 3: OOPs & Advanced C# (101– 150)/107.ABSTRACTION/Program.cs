using System;

abstract class Shape
{
    public abstract void draw();
}

class Circle : Shape
{
    public override void draw()
    {
        Console.WriteLine("Drawing Circle");
    }
}

class Program
{
    static void Main()
    {
        Shape s = new Circle();
        s.draw();
    }
}