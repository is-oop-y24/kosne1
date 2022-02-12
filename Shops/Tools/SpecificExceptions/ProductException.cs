namespace Shops.Tools.SpecificExceptions
{
    public class ProductException : ShopsException
    {
        public ProductException()
        {
        }

        public ProductException(string message)
            : base(message)
        {
        }

        public ProductException(string message, ProductException innerException)
            : base(message, innerException)
        {
        }
    }
}