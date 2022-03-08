using System;

namespace BackupsExtra.Tools.SpecificExceptions
{
    public class RestorePointException : Exception
    {
        public RestorePointException()
        {
        }

        public RestorePointException(string message)
            : base(message)
        {
        }

        public RestorePointException(string message, BackupsException innerException)
            : base(message, innerException)
        {
        }
    }
}