namespace IsuExtra.Tools.SpecificExceptions
{
    public class WeekScheduleException : IsuExtraException
    {
        public WeekScheduleException()
        {
        }

        public WeekScheduleException(string message)
            : base(message)
        {
        }

        public WeekScheduleException(string message, IsuExtraException innerException)
            : base(message, innerException)
        {
        }
    }
}