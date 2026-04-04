using System;

class Sample
{
    private int[] arr = new int[5];

    public int this[int index]
    {
        get { return arr[index]; }
        set { arr[index] = value; }
    }
}

class Program
{
    static void Main()
    {
        Sample s = new Sample();

        for (int i = 0; i < 5; i++)
            s[i] = i * 10;

        for (int i = 0; i < 5; i++)
            Console.Write(s[i] + " ");
    }
}