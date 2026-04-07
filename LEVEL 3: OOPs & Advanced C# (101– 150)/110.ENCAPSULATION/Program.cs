using System;

class Student
{
    private string name;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }
}

class Program
{
    static void Main()
    {
        Student s = new Student();
        s.Name = "Kishor";
        Console.WriteLine(s.Name);
    }
}