using System;
using Serilog;

class Program
{
    static void Main()
    {
        // Configure Logger
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        Log.Information("Application Started");

        try
        {
            int a = 10, b = 0;
            int result = a / b; // error
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Exception occurred");
        }

        Log.Information("Application Ended");

        Log.CloseAndFlush();
    }
}