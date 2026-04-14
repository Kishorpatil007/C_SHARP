using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Stack<int> stack = new Stack<int>();
        stack.Push(10);
        stack.Push(20);
        stack.Push(30);

        Console.WriteLine("Stack:");
        foreach (int i in stack)
        {
            Console.WriteLine(i);
        }

        Console.WriteLine("Pop: " + stack.Pop());

        Queue<int> queue = new Queue<int>();
        queue.Enqueue(100);
        queue.Enqueue(200);
        queue.Enqueue(300);

        Console.WriteLine("Queue:");
        foreach (int i in queue)
        {
            Console.WriteLine(i);
        }

        Console.WriteLine("Dequeue: " + queue.Dequeue());
    }
}