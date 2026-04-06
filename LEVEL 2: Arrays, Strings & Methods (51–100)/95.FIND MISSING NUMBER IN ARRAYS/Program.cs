using System;

class Program
{
    static void Main()
    {
        int[] arr = { 1, 2, 4, 5 };
        int n = 5;

        int total = n * (n + 1) / 2;
        int sum = 0;

        for (int i = 0; i < arr.Length; i++)
        {
            sum += arr[i];
        }

        int missing = total - sum;

        Console.WriteLine("Missing Number: " + missing);
    }
}