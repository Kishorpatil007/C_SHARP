using System;

class Program
{
    static void Main()
    {
        string str = "Hello world from CSharp";
        string[] words = str.Split(' ');

        foreach (string word in words)
            Console.WriteLine(word);
    }
}