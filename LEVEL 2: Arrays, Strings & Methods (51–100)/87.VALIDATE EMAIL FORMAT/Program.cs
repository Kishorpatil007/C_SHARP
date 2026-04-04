using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string email = "test@gmail.com";
        bool isValid = Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        Console.WriteLine(isValid ? "Valid Email" : "Invalid Email");
    }
}