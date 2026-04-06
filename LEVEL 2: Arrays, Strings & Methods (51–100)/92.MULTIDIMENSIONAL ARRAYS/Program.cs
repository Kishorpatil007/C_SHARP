using System;

class Program
{
    static void Main()
    {
        int[,] arr = {
            {1, 2, 3},
            {4, 5, 6}
        };

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write(arr[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}