using System;

class Program
{
    static void Display(string name, int age)
    {
        Console.WriteLine(name + " " + age);
    }

    static void Main()
    {
        Display(age: 20, name: "Kishor");
    }
}