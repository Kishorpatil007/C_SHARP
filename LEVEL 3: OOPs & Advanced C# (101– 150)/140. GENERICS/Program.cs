using System;

class GenericClass<T>
{
    public T value;

    public void Show()
    {
        Console.WriteLine(value);
    }
}

class Program
{
    static void Main()
    {
        GenericClass<int> obj1 = new GenericClass<int>();
        obj1.value = 10;
        obj1.Show();

        GenericClass<string> obj2 = new GenericClass<string>();
        obj2.value = "Hello";
        obj2.Show();
    }
}