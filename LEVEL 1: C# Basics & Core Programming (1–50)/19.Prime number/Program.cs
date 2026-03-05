using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter starting number: ");
        int start = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter ending number: ");
        int end = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Prime numbers between " + start + " and " + end + ":");

        for (int i = start; i <= end; i++)
        {
            int count = 0;

            for (int j = 1; j <= i; j++)
            {
                if (i % j == 0)
                    count++;
            }

            if (count == 2)
                Console.Write(i + " ");
        }
    }
}