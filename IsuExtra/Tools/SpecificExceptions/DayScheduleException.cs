namespace IsuExtra.Tools.SpecificExceptions
{
    public class DayScheduleException : IsuExtraException
    {
        public DayScheduleException()
        {
        }

        public DayScheduleException(string message)
            : base(message)
        {
        }

        public DayScheduleException(string message, IsuExtraException innerException)
            : base(message, innerException)
        {
        }
    }
}