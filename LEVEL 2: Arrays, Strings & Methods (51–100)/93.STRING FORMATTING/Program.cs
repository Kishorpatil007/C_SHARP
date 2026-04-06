using System;

class Program
{
    static void Main()
    {
        string name = "Kishor";
        int age = 20;
        double marks = 85.75;

        Console.WriteLine("Name: {0}, Age: {1}, Marks: {2:F2}", name, age, marks);
        Console.WriteLine($"Name: {name}, Age: {age}, Marks: {marks}");
    }
}