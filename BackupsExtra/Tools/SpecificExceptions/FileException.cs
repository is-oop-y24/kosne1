using System;

namespace BackupsExtra.Tools.SpecificExceptions
{
    public class FileException : Exception
    {
        public FileException()
        {
        }

        public FileException(string message)
            : base(message)
        {
        }

        public FileException(string message, BackupsException innerException)
            : base(message, innerException)
        {
        }
    }
}