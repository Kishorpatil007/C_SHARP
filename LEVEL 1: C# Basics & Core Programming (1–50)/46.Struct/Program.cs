using System;

struct Student
{
    public int Id;
    public string Name;

    public void Display()
    {
        Console.WriteLine("Id: " + Id);
        Console.WriteLine("Name: " + Name);
    }
}

class StructDemo
{
    static void Main()
    {
        Student s;
        s.Id = 20;
        s.Name = "Kishor";
        s.Display();
    }
}