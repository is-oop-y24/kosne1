namespace IsuExtra.Tools.SpecificExceptions
{
    public class GroupException : IsuExtraException
    {
        public GroupException()
        {
        }

        public GroupException(string message)
            : base(message)
        {
        }

        public GroupException(string message, IsuExtraException innerException)
            : base(message, innerException)
        {
        }
    }
}