using System;

class Program
{
    static int Sum(params int[] nums)
    {
        int s = 0;
        foreach (int n in nums)
            s += n;
        return s;
    }

    static void Main()
    {
        Console.WriteLine(Sum(1, 2, 3, 4));
    }
}