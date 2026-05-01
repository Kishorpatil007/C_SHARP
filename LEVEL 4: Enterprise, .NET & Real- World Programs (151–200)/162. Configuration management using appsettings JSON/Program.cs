using System;
using Microsoft.Extensions.Configuration;
using System.IO;

class Program
{
    static void Main()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        string appName = config["AppSettings:AppName"];
        string version = config["AppSettings:Version"];

        Console.WriteLine("App Name: " + appName);
        Console.WriteLine("Version: " + version);
    }
}