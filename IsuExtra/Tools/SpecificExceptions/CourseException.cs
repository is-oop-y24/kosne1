namespace IsuExtra.Tools.SpecificExceptions
{
    public class CourseException : IsuExtraException
    {
        public CourseException()
        {
        }

        public CourseException(string message)
            : base(message)
        {
        }

        public CourseException(string message, IsuExtraException innerException)
            : base(message, innerException)
        {
        }
    }
}