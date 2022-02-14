namespace IsuExtra.Tools.SpecificExceptions
{
    public class AECourseException : IsuExtraException
    {
        public AECourseException()
        {
        }

        public AECourseException(string message)
            : base(message)
        {
        }

        public AECourseException(string message, IsuExtraException innerException)
            : base(message, innerException)
        {
        }
    }
}