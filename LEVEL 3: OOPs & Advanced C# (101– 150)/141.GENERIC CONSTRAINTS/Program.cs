using System;

class Test<T> where T : class
{
    public void Show(T item)
    {
        Console.WriteLine(item);
    }
}

class Program
{
    static void Main()
    {
        Test<string> obj = new Test<string>();
        obj.Show("Constraint Example");
    }
}