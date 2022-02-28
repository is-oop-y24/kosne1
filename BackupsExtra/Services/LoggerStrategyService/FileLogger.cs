using System.IO;

namespace BackupsExtra.Services.LoggerStrategyService
{
    public class FileLogger : ILogger
    {
        private string pathToLogger;

        public FileLogger(string pathToLogger)
        {
            this.pathToLogger = pathToLogger;
        }

        public void ErrorLogging(string message)
        {
            File.AppendAllLines(pathToLogger, new[] { "Error: " + message });
        }

        public void WarningLogging(string message)
        {
            File.AppendAllLines(pathToLogger, new[] { "Error: " + message });
        }

        public void InformationLogging(string message)
        {
            File.AppendAllLines(pathToLogger, new[] { "Error: " + message });
        }
    }
}