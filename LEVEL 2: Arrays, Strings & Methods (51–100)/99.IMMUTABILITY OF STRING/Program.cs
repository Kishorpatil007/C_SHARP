using System;

class Program
{
    static void Main()
    {
        string str = "Hello";
        string newStr = str;

        newStr = newStr + " World";

        Console.WriteLine(str);
        Console.WriteLine(newStr);
    }
}