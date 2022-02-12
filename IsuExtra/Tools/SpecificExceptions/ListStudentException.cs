namespace IsuExtra.Tools.SpecificExceptions
{
    public class ListStudentException : IsuExtraException
    {
        public ListStudentException()
        {
        }

        public ListStudentException(string message)
            : base(message)
        {
        }

        public ListStudentException(string message, IsuExtraException innerException)
            : base(message, innerException)
        {
        }
    }
}