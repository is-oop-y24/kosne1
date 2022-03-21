using System;
using Banks.Tools.Exceptions;

namespace Banks.Tools.SpecificExceptions
{
    public class TransactionException : BanksException
    {
        public TransactionException()
        {
        }

        public TransactionException(string message)
            : base(message)
        {
        }

        public TransactionException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}