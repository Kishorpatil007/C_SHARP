using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter number of elements: ");
        int n = Convert.ToInt32(Console.ReadLine());
        int[] arr = new int[n];
        int sum = 0;

        Console.WriteLine("Enter elements:");
        for (int i = 0; i < n; i++)
        {
            arr[i] = Convert.ToInt32(Console.ReadLine());
            sum += arr[i];
        }

        double avg = (double)sum / n;

        Console.WriteLine("Sum = " + sum);
        Console.WriteLine("Average = " + avg);
    }
}