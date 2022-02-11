namespace Shops.Tools.SpecificExceptions
{
    public class TransferException : ShopsException
    {
        public TransferException()
        {
        }

        public TransferException(string message)
            : base(message)
        {
        }

        public TransferException(string message, ShopsException innerException)
            : base(message, innerException)
        {
        }
    }
}