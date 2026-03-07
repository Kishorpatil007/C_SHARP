using System;

class TupleDemo
{
    static void Main()
    {
        var person = (Id: 20, Name: "Kishor", Age: 22);

        Console.WriteLine("Id: " + person.Id);
        Console.WriteLine("Name: " + person.Name);
        Console.WriteLine("Age: " + person.Age);
    }
}