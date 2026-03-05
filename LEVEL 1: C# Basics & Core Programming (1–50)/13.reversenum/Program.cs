using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter a number: ");
        int num = Convert.ToInt32(Console.ReadLine());

        int reverse = 0;

        while (num != 0)
        {
            int remainder = num % 10;
            reverse = reverse * 10 + remainder;
            num /= 10;
        }

        Console.WriteLine("Reversed Number: " + reverse);
    }
}