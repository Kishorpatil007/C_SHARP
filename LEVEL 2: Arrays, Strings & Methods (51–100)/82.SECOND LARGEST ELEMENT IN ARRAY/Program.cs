using System;

class Program
{
    static void Main()
    {
        int[] arr = { 12, 35, 1, 10, 34, 1 };
        int largest = int.MinValue, second = int.MinValue;

        foreach (int num in arr)
        {
            if (num > largest)
            {
                second = largest;
                largest = num;
            }
            else if (num > second && num != largest)
            {
                second = num;
            }
        }

        Console.WriteLine(second);
    }
}