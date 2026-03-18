using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter size of first array: ");
        int n1 = Convert.ToInt32(Console.ReadLine());
        int[] arr1 = new int[n1];

        Console.WriteLine("Enter elements of first array:");
        for (int i = 0; i < n1; i++)
        {
            arr1[i] = Convert.ToInt32(Console.ReadLine());
        }

        Console.Write("Enter size of second array: ");
        int n2 = Convert.ToInt32(Console.ReadLine());
        int[] arr2 = new int[n2];

        Console.WriteLine("Enter elements of second array:");
        for (int i = 0; i < n2; i++)
        {
            arr2[i] = Convert.ToInt32(Console.ReadLine());
        }

        int[] merged = new int[n1 + n2];

        for (int i = 0; i < n1; i++)
            merged[i] = arr1[i];

        for (int i = 0; i < n2; i++)
            merged[n1 + i] = arr2[i];

        Console.WriteLine("Merged array:");
        for (int i = 0; i < merged.Length; i++)
            Console.Write(merged[i] + " ");
    }
}