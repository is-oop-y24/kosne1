using System;

namespace IsuExtra.Tools.SpecificExceptions
{
    public class GroupNameException : IsuExtraException
    {
        public GroupNameException()
        {
        }

        public GroupNameException(string message)
            : base(message)
        {
        }

        public GroupNameException(string message, IsuExtraException innerException)
            : base(message, innerException)
        {
        }
    }
}