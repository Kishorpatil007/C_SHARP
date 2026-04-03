using System;
using System.Text;

class Program
{
    static void Main()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Hello");
        sb.Append(" ");
        sb.Append("World");
        sb.Insert(5, " CSharp");
        sb.Replace("World", "Programming");
        Console.WriteLine(sb.ToString());
    }
}