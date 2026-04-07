using System;

class Student
{
    public string name;
    public int age;

    public Student(string n, int a)
    {
        name = n;
        age = a;
    }

    public void display()
    {
        Console.WriteLine(name + " " + age);
    }
}

class Program
{
    static void Main()
    {
        Student s = new Student("Kishor", 20);
        s.display();
    }
}