using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter base number: ");
        int num = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter power: ");
        int power = Convert.ToInt32(Console.ReadLine());

        double result = Math.Pow(num, power);

        Console.WriteLine("Result: " + result);
    }
}