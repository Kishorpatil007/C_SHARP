using System;
using System.Threading.Tasks;

class Program
{
    static async Task<int> CalculateAsync(int a, int b)
    {
        await Task.Delay(1000);
        return a + b;
    }

    static async Task Main()
    {
        int a = 5;
        int b = 7;
        int result = await CalculateAsync(a, b);
        Console.WriteLine(result);
    }
}