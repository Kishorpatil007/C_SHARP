using System;

class Program
{
    static void Main()
    {
        Random r = new Random();

        Console.Write("How many random numbers: ");
        int n = Convert.ToInt32(Console.ReadLine());

        for (int i = 1; i <= n; i++)
        {
            Console.WriteLine(r.Next(1, 101));
        }
    }
}