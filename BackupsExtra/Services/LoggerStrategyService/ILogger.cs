using BackupsExtra.Services.LoggerStrategyService.TimeStrategyService;

namespace BackupsExtra.Services.LoggerStrategyService
{
    public interface ILogger
    {
        public ITime TimeStrategy { get; set; }

        void ErrorLogging(string message);
        void WarningLogging(string message);
        void InformationLogging(string message);
    }
}