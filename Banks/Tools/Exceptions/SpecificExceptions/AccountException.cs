namespace Banks.Tools.Exceptions.SpecificExceptions
{
    public class AccountException : BanksException
    {
        public AccountException()
        {
        }

        public AccountException(string message)
            : base(message)
        {
        }

        public AccountException(string message, BanksException innerException)
            : base(message, innerException)
        {
        }
    }
}