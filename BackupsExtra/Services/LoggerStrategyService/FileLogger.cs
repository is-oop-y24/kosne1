using System.IO;
using BackupsExtra.Services.LoggerStrategyService.TimeStrategyService;

namespace BackupsExtra.Services.LoggerStrategyService
{
    public class FileLogger : ILogger
    {
        private string pathToLogger;

        public FileLogger(string pathToLogger)
        {
            this.pathToLogger = pathToLogger;
        }

        public ITime TimeStrategy { get; set; }

        public void ErrorLogging(string message)
        {
            File.AppendAllLines(pathToLogger, new[] { TimeStrategy.TimeStrategy() + "Error: " + message });
        }

        public void WarningLogging(string message)
        {
            File.AppendAllLines(pathToLogger, new[] { TimeStrategy.TimeStrategy() + "Error: " + message });
        }

        public void InformationLogging(string message)
        {
            File.AppendAllLines(pathToLogger, new[] { TimeStrategy.TimeStrategy() + "Error: " + message });
        }
    }
}