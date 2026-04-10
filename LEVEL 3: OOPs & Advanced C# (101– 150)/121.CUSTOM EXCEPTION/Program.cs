using System;

class InvalidAgeException : Exception
{
    public InvalidAgeException(string message) : base(message)
    {
    }
}

class Program
{
    static void CheckAge(int age)
    {
        if(age < 18)
            throw new InvalidAgeException("Age must be 18 or older.");
        else
            Console.WriteLine("Age is valid: " + age);
    }

    static void Main()
    {
        try
        {
            Console.WriteLine("Enter age:");
            int age = Convert.ToInt32(Console.ReadLine());
            CheckAge(age);
        }
        catch(InvalidAgeException ex)
        {
            Console.WriteLine("Custom Exception: " + ex.Message);
        }
    }
}