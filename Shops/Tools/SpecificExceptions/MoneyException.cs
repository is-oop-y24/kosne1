namespace Shops.Tools.SpecificExceptions
{
    public class MoneyException : ShopsException
    {
        public MoneyException()
        {
        }

        public MoneyException(string message)
            : base(message)
        {
        }

        public MoneyException(string message, ShopsException innerException)
            : base(message, innerException)
        {
        }
    }
}