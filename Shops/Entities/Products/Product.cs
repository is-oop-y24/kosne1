using System;

namespace Shops.Entities.Products
{
    public class Product
    {
        public Product(ProductName productName, decimal money, int count)
        {
            ProductName = productName;
            Price = money;
            Count = count;
        }

        public ProductName ProductName { get; }
        public decimal Price { get; set; }
        public int Count { get; set; }

        public decimal TotalCost()
        {
            return Price * Count;
        }
    }
}