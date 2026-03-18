using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter rows and columns: ");
        int r = Convert.ToInt32(Console.ReadLine());
        int c = Convert.ToInt32(Console.ReadLine());

        int[,] a = new int[r, c];
        int[,] b = new int[r, c];
        int[,] sum = new int[r, c];

        Console.WriteLine("Enter first matrix:");
        for (int i = 0; i < r; i++)
        {
            for (int j = 0; j < c; j++)
            {
                a[i, j] = Convert.ToInt32(Console.ReadLine());
            }
        }

        Console.WriteLine("Enter second matrix:");
        for (int i = 0; i < r; i++)
        {
            for (int j = 0; j < c; j++)
            {
                b[i, j] = Convert.ToInt32(Console.ReadLine());
            }
        }

        for (int i = 0; i < r; i++)
        {
            for (int j = 0; j < c; j++)
            {
                sum[i, j] = a[i, j] + b[i, j];
            }
        }

        Console.WriteLine("Sum matrix:");
        for (int i = 0; i < r; i++)
        {
            for (int j = 0; j < c; j++)
            {
                Console.Write(sum[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}