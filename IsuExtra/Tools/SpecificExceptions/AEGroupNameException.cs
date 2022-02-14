namespace IsuExtra.Tools.SpecificExceptions
{
    public class AEGroupNameException : IsuExtraException
    {
        public AEGroupNameException()
        {
        }

        public AEGroupNameException(string message)
            : base(message)
        {
        }

        public AEGroupNameException(string message, IsuExtraException innerException)
            : base(message, innerException)
        {
        }
    }
}