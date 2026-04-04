using System;

class Program
{
    static void PrintArray(int[] arr)
    {
        foreach (int i in arr)
            Console.Write(i + " ");
    }

    static void Main()
    {
        int[] arr = { 10, 20, 30, 40 };
        PrintArray(arr);
    }
}