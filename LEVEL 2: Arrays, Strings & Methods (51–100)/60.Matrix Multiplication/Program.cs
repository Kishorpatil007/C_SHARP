using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter rows and columns of first matrix: ");
        int r1 = Convert.ToInt32(Console.ReadLine());
        int c1 = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter rows and columns of second matrix: ");
        int r2 = Convert.ToInt32(Console.ReadLine());
        int c2 = Convert.ToInt32(Console.ReadLine());

        int[,] a = new int[r1, c1];
        int[,] b = new int[r2, c2];
        int[,] result = new int[r1, c2];

        Console.WriteLine("Enter first matrix:");
        for (int i = 0; i < r1; i++)
        {
            for (int j = 0; j < c1; j++)
            {
                a[i, j] = Convert.ToInt32(Console.ReadLine());
            }
        }

        Console.WriteLine("Enter second matrix:");
        for (int i = 0; i < r2; i++)
        {
            for (int j = 0; j < c2; j++)
            {
                b[i, j] = Convert.ToInt32(Console.ReadLine());
            }
        }

        if (c1 == r2)
        {
            for (int i = 0; i < r1; i++)
            {
                for (int j = 0; j < c2; j++)
                {
                    result[i, j] = 0;
                    for (int k = 0; k < c1; k++)
                    {
                        result[i, j] += a[i, k] * b[k, j];
                    }
                }
            }

            Console.WriteLine("Result matrix:");
            for (int i = 0; i < r1; i++)
            {
                for (int j = 0; j < c2; j++)
                {
                    Console.Write(result[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("Matrix multiplication not possible");
        }
    }
}