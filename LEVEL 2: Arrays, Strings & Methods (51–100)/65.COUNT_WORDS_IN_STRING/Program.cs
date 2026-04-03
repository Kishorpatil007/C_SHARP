using System;

class Program
{
    static void Main()
    {
        string str = Console.ReadLine();
        string[] words = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        Console.WriteLine(words.Length);
    }
}