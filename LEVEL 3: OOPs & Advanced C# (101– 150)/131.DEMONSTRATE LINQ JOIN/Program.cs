using System;
using System.Linq;

class Student
{
    public int Id;
    public string Name;
}

class Course
{
    public int StudentId;
    public string CourseName;
}

class Program
{
    static void Main()
    {
        Student[] students =
        {
            new Student{Id=1, Name="Amit"},
            new Student{Id=2, Name="Rahul"},
            new Student{Id=3, Name="Sneha"}
        };

        Course[] courses =
        {
            new Course{StudentId=1, CourseName="C#"},
            new Course{StudentId=2, CourseName="Java"},
            new Course{StudentId=1, CourseName="SQL"}
        };

        var result = students.Join(
            courses,
            s => s.Id,
            c => c.StudentId,
            (s, c) => new { s.Name, c.CourseName }
        );

        foreach (var item in result)
        {
            Console.WriteLine(item.Name + " " + item.CourseName);
        }
    }
}