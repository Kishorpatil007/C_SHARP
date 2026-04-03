using System;

class Program
{
    static void Main()
    {
        string str = Console.ReadLine();
        string oldWord = Console.ReadLine();
        string newWord = Console.ReadLine();
        string result = str.Replace(oldWord, newWord);
        Console.WriteLine(result);
    }
}