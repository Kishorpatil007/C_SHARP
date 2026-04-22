using System;
using System.IO;

class Program
{
    static void Main()
    {
        string path = "test.txt";

        File.WriteAllText(path, "Hello File Handling");

        string data = File.ReadAllText(path);

        Console.WriteLine(data);
    }
}