namespace Isu.Tools.SpecificExceptions
{
    public class GroupException : IsuException
    {
        public GroupException()
        {
        }

        public GroupException(string message)
            : base(message)
        {
        }

        public GroupException(string message, IsuException innerException)
            : base(message, innerException)
        {
        }
    }
}