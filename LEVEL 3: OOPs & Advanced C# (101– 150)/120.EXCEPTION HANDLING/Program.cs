using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter a number:");
        try
        {
            int num = Convert.ToInt32(Console.ReadLine());
            int result = 100 / num;
            Console.WriteLine("Result: " + result);
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("Cannot divide by zero");
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input format");
        }
        finally
        {
            Console.WriteLine("Execution completed");
        }
    }
}