using System;

namespace BackupsExtra.Services.LoggerStrategyService
{
    public class ConsoleLogger : ILogger
    {
        public void ErrorLogging(string message)
        {
            Console.WriteLine("Error: " + message);
        }

        public void WarningLogging(string message)
        {
            Console.WriteLine("Warning: " + message);
        }

        public void InformationLogging(string message)
        {
            Console.WriteLine("Information: " + message);
        }
    }
}