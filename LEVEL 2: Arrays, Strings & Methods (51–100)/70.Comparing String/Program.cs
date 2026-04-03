using System;

class Program
{
    static void Main()
    {
        string str1 = Console.ReadLine();
        string str2 = Console.ReadLine();

        if (str1.Equals(str2))
            Console.WriteLine("Equal");
        else
            Console.WriteLine("Not Equal");

        int result = string.Compare(str1, str2);
        Console.WriteLine(result);
    }
}