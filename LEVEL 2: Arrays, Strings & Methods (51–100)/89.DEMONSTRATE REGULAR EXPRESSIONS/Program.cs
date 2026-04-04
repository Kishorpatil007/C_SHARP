using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string text = "My number is 9876543210";
        Match match = Regex.Match(text, @"\d+");

        Console.WriteLine(match.Value);
    }
}