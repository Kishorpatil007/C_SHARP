using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter a number: ");
        int n = Convert.ToInt32(Console.ReadLine());
        int i = 1;

        while (i <= n)
        {
            Console.WriteLine(i);
            i++;
        }
    }
}