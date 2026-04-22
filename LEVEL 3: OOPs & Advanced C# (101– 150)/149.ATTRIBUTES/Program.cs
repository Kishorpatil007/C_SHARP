using System;

[AttributeUsage(AttributeTargets.Class)]
class InfoAttribute : Attribute
{
    public string Name;

    public InfoAttribute(string name)
    {
        Name = name;
    }
}

[Info("Custom Attribute Example")]
class Demo
{
}

class Program
{
    static void Main()
    {
        Type t = typeof(Demo);
        object[] attrs = t.GetCustomAttributes(false);

        foreach (object attr in attrs)
        {
            InfoAttribute info = (InfoAttribute)attr;
            Console.WriteLine(info.Name);
        }
    }
}