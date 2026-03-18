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

        Console.WriteLine("Array after removing duplicates:");
        for (int i = 0; i < n; i++)
        {
            bool isDuplicate = false;
            for (int j = 0; j < i; j++)
            {
                if (arr[i] == arr[j])
                {
                    isDuplicate = true;
                    break;
                }
            }
            if (!isDuplicate)
                Console.Write(arr[i] + " ");
        }
    }
}