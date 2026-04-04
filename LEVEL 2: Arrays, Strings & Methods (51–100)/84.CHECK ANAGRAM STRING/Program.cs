using System;

class Program
{
    static void Main()
    {
        string str1 = "listen";
        string str2 = "silent";

        char[] a = str1.ToLower().ToCharArray();
        char[] b = str2.ToLower().ToCharArray();

        Array.Sort(a);
        Array.Sort(b);

        string s1 = new string(a);
        string s2 = new string(b);

        if (s1 == s2)
            Console.WriteLine("Anagram");
        else
            Console.WriteLine("Not Anagram");
    }
}