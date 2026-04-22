using System;
using System.Text.Json;

class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
}

class Program
{
    static void Main()
    {
        Student s = new Student { Id = 1, Name = "Kishor" };
        string json = JsonSerializer.Serialize(s);
        Console.WriteLine(json);

        Student obj = JsonSerializer.Deserialize<Student>(json);
        Console.WriteLine(obj.Name);
    }
}