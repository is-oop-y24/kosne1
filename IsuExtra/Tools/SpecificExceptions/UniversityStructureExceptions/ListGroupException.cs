namespace IsuExtra.Tools.SpecificExceptions.UniversityStructureExceptions
{
    public class ListGroupException : IsuExtraException
    {
        public ListGroupException()
        {
        }

        public ListGroupException(string message)
            : base(message)
        {
        }

        public ListGroupException(string message, IsuExtraException innerException)
            : base(message, innerException)
        {
        }
    }
}