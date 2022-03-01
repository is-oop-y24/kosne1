using System;
using BackupsExtra.Services.LoggerStrategyService.TimeStrategyService;

namespace BackupsExtra.Services.LoggerStrategyService
{
    public class ConsoleLogger : ILogger
    {
        public ITime TimeStrategy { get; set; }

        public void ErrorLogging(string message)
        {
            Console.WriteLine(TimeStrategy.TimeStrategy() + "Error: " + message);
        }

        public void WarningLogging(string message)
        {
            Console.WriteLine(TimeStrategy.TimeStrategy() + "Warning: " + message);
        }

        public void InformationLogging(string message)
        {
            Console.WriteLine(TimeStrategy.TimeStrategy() + "Information: " + message);
        }
    }
}