namespace Isu.Tools.SpecificExceptions
{
    public class StudentException : IsuException
    {
        public StudentException()
        {
        }

        public StudentException(string message)
            : base(message)
        {
        }

        public StudentException(string message, IsuException innerException)
            : base(message, innerException)
        {
        }
    }
}