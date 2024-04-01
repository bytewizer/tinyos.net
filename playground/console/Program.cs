using System;

namespace RoslynOS.Template
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

            Console.WriteLine($"Environment: {env}");

#if DEBUG
            Console.WriteLine("Configuration: Debug");
#else
            Console.WriteLine("Configuration: Release");
#endif
        }
    }
}
