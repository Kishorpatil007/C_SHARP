using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string password = "Abc@1234";
        bool isValid = Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&+=]).{8,}$");

        Console.WriteLine(isValid ? "Strong Password" : "Weak Password");
    }
}