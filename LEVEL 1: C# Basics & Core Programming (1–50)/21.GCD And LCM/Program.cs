using System;

class Program
{
    static void Main()
    {
        int a, b, gcd = 1, lcm;

        Console.Write("Enter first number: ");
        a = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter second number: ");
        b = Convert.ToInt32(Console.ReadLine());

        for (int i = 1; i <= a && i <= b; i++)
        {
            if (a % i == 0 && b % i == 0)
                gcd = i;
        }

        lcm = (a * b) / gcd;

        Console.WriteLine("GCD = " + gcd);
        Console.WriteLine("LCM = " + lcm);
    }
}