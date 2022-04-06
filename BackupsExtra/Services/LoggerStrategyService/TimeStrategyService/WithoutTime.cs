namespace BackupsExtra.Services.LoggerStrategyService.TimeStrategyService
{
    public class WithoutTime : ITime
    {
        public string TimeStrategy()
        {
            return string.Empty;
        }
    }
}