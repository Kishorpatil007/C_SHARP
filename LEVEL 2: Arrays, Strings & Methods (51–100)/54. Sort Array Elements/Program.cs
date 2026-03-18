using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter number of elements: ");
        int n = Convert.ToInt32(Console.ReadLine());
        int[] arr = new int[n];

        Console.WriteLine("Enter elements:");
        for (int i = 0; i < n; i++)
        {
            arr[i] = Convert.ToInt32(Console.ReadLine());
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                if (arr[i] > arr[j])
                {
                    int temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }
        }

        Console.WriteLine("Sorted array:");
        for (int i = 0; i < n; i++)
        {
            Console.Write(arr[i] + " ");
        }
    }
}