using Banks.Tools.Exceptions;

namespace Banks.Tools.SpecificExceptions
{
    public class BankException : BanksException
    {
        public BankException()
        {
        }

        public BankException(string message)
            : base(message)
        {
        }

        public BankException(string message, BanksException innerException)
            : base(message, innerException)
        {
        }
    }
}