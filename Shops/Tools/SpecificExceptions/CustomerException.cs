namespace Shops.Tools.SpecificExceptions
{
    public class CustomerException : ShopsException
    {
        public CustomerException()
        {
        }

        public CustomerException(string message)
            : base(message)
        {
        }

        public CustomerException(string message, ShopsException innerException)
            : base(message, innerException)
        {
        }
    }
}