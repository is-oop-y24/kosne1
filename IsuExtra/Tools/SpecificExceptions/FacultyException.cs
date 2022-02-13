namespace IsuExtra.Tools.SpecificExceptions
{
    public class FacultyException : IsuExtraException
    {
        public FacultyException()
        {
        }

        public FacultyException(string message)
            : base(message)
        {
        }

        public FacultyException(string message, IsuExtraException innerException)
            : base(message, innerException)
        {
        }
    }
}