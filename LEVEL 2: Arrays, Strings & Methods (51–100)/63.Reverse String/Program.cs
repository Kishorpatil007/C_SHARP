using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter a string: ");
        string str = Console.ReadLine();
        string rev = "";

        for (int i = str.Length - 1; i >= 0; i--)
        {
            rev += str[i];
        }

        Console.WriteLine("Reversed string = " + rev);
    }
}