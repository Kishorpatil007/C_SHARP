using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter a number: ");
        int n = Convert.ToInt32(Console.ReadLine());
        int i = 1;

        do
        {
            Console.WriteLine(i);
            i++;
        } while (i <= n);
    }
}