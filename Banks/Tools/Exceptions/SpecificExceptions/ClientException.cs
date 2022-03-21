using Banks.Tools.Exceptions;

namespace Banks.Tools.SpecificExceptions
{
    public class ClientException : BanksException
    {
        public ClientException()
        {
        }

        public ClientException(string message)
            : base(message)
        {
        }

        public ClientException(string message, BanksException innerException)
            : base(message, innerException)
        {
        }
    }
}