namespace IsuExtra.Tools.SpecificExceptions
{
    public class AEGroupException : IsuExtraException
    {
        public AEGroupException()
        {
        }

        public AEGroupException(string message)
            : base(message)
        {
        }

        public AEGroupException(string message, IsuExtraException innerException)
            : base(message, innerException)
        {
        }
    }
}