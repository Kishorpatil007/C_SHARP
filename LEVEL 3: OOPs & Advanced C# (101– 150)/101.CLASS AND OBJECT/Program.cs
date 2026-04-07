using System;

class Student
{
    public string name;
    public int age;

    public void display()
    {
        Console.WriteLine("Name: " + name);
        Console.WriteLine("Age: " + age);
    }
}

class Program
{
    static void Main()
    {
        Student s = new Student();
        s.name = "Kishor";
        s.age = 20;
        s.display();
    }
}