using System;

class Program
{
    static void ValidateNumber(int num)
    {
        if(num < 0)
            throw new ArgumentException("Number cannot be negative");
        else
            Console.WriteLine("Valid number: " + num);
    }

    static void Main()
    {
        try
        {
            Console.WriteLine("Enter a number:");
            int number = Convert.ToInt32(Console.ReadLine());
            ValidateNumber(number);
        }
        catch(Exception ex)
        {
            Console.WriteLine("Exception: " + ex.Message);
        }
    }
}