using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter a number: ");
        int num = Convert.ToInt32(Console.ReadLine());

        if (num > 0)
            Console.WriteLine("Positive Number");     
        else if (num < 0)
            Console.WriteLine("Negative Number");
        else
            Console.WriteLine("Zero");
    }
}