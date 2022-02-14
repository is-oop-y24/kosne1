namespace IsuExtra.Tools.SpecificExceptions.UniversityPeopleException
{
    public class StudentException : IsuExtraException
    {
        public StudentException()
        {
        }

        public StudentException(string message)
            : base(message)
        {
        }

        public StudentException(string message, IsuExtraException innerException)
            : base(message, innerException)
        {
        }
    }
}