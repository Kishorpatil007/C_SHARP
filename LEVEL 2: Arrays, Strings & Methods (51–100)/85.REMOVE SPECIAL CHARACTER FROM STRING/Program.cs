using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string str = "Hello@# World!! 123";
        string result = Regex.Replace(str, "[^a-zA-Z0-9 ]", "");

        Console.WriteLine(result);
    }
}