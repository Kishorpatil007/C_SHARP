using System;
using System.Reflection;

class Demo
{
    public void Show()
    {
        Console.WriteLine("Hello Reflection");
    }
}

class Program
{
    static void Main()
    {
        Type t = typeof(Demo);
        object obj = Activator.CreateInstance(t);
        MethodInfo m = t.GetMethod("Show");
        m.Invoke(obj, null);
    }
}