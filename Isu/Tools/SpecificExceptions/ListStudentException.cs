namespace Isu.Tools.SpecificExceptions
{
    public class ListStudentException : IsuException
    {
        public ListStudentException()
        {
        }

        public ListStudentException(string message)
            : base(message)
        {
        }

        public ListStudentException(string message, IsuException innerException)
            : base(message, innerException)
        {
        }
    }
}