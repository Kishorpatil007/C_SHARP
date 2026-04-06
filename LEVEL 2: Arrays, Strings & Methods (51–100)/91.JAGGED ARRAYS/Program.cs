using System;

class Program
{
    static void Main()
    {
        int[][] arr = new int[3][];

        arr[0] = new int[] { 1, 2 };
        arr[1] = new int[] { 3, 4, 5 };
        arr[2] = new int[] { 6 };

        for (int i = 0; i < arr.Length; i++)
        {
            for (int j = 0; j < arr[i].Length; j++)
            {
                Console.Write(arr[i][j] + " ");
            }
            Console.WriteLine();
        }
    }
}