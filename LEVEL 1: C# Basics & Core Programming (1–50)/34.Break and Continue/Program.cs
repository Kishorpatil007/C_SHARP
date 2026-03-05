using System;

class Program
{
    static void Main()
    {
        for (int i = 1; i <= 10; i++)
        {
            if (i == 5)
                continue;

            if (i == 8)
                break;

            Console.WriteLine(i);
        }
    }
}