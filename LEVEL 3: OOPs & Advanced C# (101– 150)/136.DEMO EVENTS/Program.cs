using System;

class Process
{
    public event Action OnProcessCompleted;

    public void Start()
    {
        Console.WriteLine("Process Started");
        OnProcessCompleted?.Invoke();
    }
}

class Program
{
    static void Main()
    {
        Process p = new Process();

        p.OnProcessCompleted += () => Console.WriteLine("Process Completed");

        p.Start();
    }
}