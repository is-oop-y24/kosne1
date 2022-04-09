namespace IsuExtra.Tools.SpecificExceptions.ScheduleExceptions
{
    public class LessonException : IsuExtraException
    {
        public LessonException()
        {
        }

        public LessonException(string message)
            : base(message)
        {
        }

        public LessonException(string message, IsuExtraException innerException)
            : base(message, innerException)
        {
        }
    }
}