using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        Stopwatch sw = new Stopwatch();

        sw.Start();
        int sum = 0;
        for (int i = 0; i < 1000000; i++)
        {
            sum += i;
        }
        sw.Stop();

        Console.WriteLine(sum);
        Console.WriteLine(sw.ElapsedMilliseconds);
    }
}