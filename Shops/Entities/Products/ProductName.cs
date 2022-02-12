using System;

namespace Shops.Entities.Products
{
    public class ProductName
    {
        public ProductName(string productName)
        {
            Id = Guid.NewGuid();
            Name = productName;
        }

        public string Name { get; }
        public Guid Id { get; }
    }
}