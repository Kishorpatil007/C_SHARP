using System;

interface IWorker
{
    void Work();
}

class Developer : IWorker
{
    public void Work()
    {
        Console.WriteLine("Developer writing code");
    }
}

class Manager : IWorker
{
    public void Work()
    {
        Console.WriteLine("Manager managing team");
    }
}

class WorkManager
{
    public void Manage(IWorker worker)
    {
        worker.Work();
    }
}

class Program
{
    static void Main()
    {
        IWorker dev = new Developer();
        IWorker mgr = new Manager();

        WorkManager manager = new WorkManager();
        manager.Manage(dev);
        manager.Manage(mgr);
    }
}