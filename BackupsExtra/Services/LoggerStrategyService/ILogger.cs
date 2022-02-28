namespace BackupsExtra.Services.LoggerStrategyService
{
    public interface ILogger
    {
        void ErrorLogging(string message);
        void WarningLogging(string message);
        void InformationLogging(string message);
    }
}