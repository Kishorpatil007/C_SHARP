using System;

interface IAnimal
{
    void sound();
}

class Dog : IAnimal
{
    public void sound()
    {
        Console.WriteLine("Bark");
    }
}

class Program
{
    static void Main()
    {
        IAnimal a = new Dog();
        a.sound();
    }
}