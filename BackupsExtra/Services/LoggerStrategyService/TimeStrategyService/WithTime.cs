using System;
using System.Globalization;

namespace BackupsExtra.Services.LoggerStrategyService.TimeStrategyService
{
    public class WithTime : ITime
    {
        public string TimeStrategy()
        {
            return DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ";
        }
    }
}