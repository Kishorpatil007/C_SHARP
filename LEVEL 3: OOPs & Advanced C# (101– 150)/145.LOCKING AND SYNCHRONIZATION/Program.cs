using System;
using System.Threading;

class Counter
{
    private int value = 0;
    private object lockObj = new object();

    public void Increment()
    {
        lock (lockObj)
        {
            value++;
        }
    }

    public void Show()
    {
        Console.WriteLine(value);
    }
}

class Program
{
    static void Main()
    {
        Counter c = new Counter();

        Thread t1 = new Thread(() =>
        {
            for (int i = 0; i < 1000; i++) c.Increment();
        });

        Thread t2 = new Thread(() =>
        {
            for (int i = 0; i < 1000; i++) c.Increment();
        });

        t1.Start();
        t2.Start();

        t1.Join();
        t2.Join();

        c.Show();
    }
}