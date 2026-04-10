using System;

class Program
{
    static void Main()
    {
        try
        {
            Console.WriteLine("Enter a number:");
            int num = Convert.ToInt32(Console.ReadLine());
            int result = 100 / num;
            Console.WriteLine("Result: " + result);
        }
        catch(Exception ex)
        {
            Console.WriteLine("Exception occurred: " + ex.Message);
        }
        finally
        {
            Console.WriteLine("Finally block executed");
        }
    }
}