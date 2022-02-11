using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Shops.Tools.SpecificExceptions;

namespace Shops.Entities.Products
{
    public class Catalog
    {
        private List<Product> _products;

        public Catalog()
        {
             _products = new List<Product>();
        }

        public Catalog(List<Product> products)
        {
            _products = products;
        }

        public List<Product> GetProducts()
        {
            return new List<Product>(_products);
        }

        public void AddProducts(List<Product> products)
        {
            foreach (Product product in products)
            {
                Product foundProduct = _products.FirstOrDefault(tempProduct => tempProduct.ProductName.Id == product.ProductName.Id);
                if (foundProduct != default)
                {
                    foundProduct.Count += product.Count;
                }
                else
                {
                    _products.Add(product);
                }
            }
        }

        public void RemoveProducts(List<Product> products)
        {
            foreach (Product product in products)
            {
                Product foundProduct = _products.FirstOrDefault(tempProduct => tempProduct.ProductName.Id == product.ProductName.Id);
                if (foundProduct != default)
                {
                    foundProduct.Count -= product.Count;
                }
            }
        }

        public decimal TotalCost()
        {
            return _products.Sum(product => product.TotalCost());
        }

        public bool HaveProducts(List<Product> products)
        {
            return products.All(product => FindProduct(product.ProductName.Id).Count >= product.Count);
        }

        public void ChangePrice(List<Product> newpProducts)
        {
            foreach (Product product in newpProducts)
            {
                Product foundProduct = _products.FirstOrDefault(tempProduct => tempProduct.ProductName.Id == product.ProductName.Id);
                if (foundProduct != default)
                {
                    foundProduct.Price = product.Price;
                }
                else
                {
                    throw new ProductException("Error: product not found");
                }
            }
        }

        public Catalog Receipt(List<Product> products)
        {
            foreach (Product product in products)
            {
                Product foundProduct = _products.FirstOrDefault(tempProduct => tempProduct.ProductName.Id == product.ProductName.Id);
                if (foundProduct != default)
                {
                    product.Price = foundProduct.Price;
                }
                else
                {
                    throw new ProductException("Error: product no found");
                }
            }

            var catalog = new Catalog(products);

            return catalog;
        }

        public Catalog ProductsForMinimumProductsValue(List<Product> products)
        {
            var catalog = new Catalog();
            foreach (Product product in products)
            {
                Product foundProduct = _products.FirstOrDefault(tempProduct => tempProduct.ProductName.Id == product.ProductName.Id);
                if (foundProduct != default)
                {
                    catalog.AddProducts(new List<Product> { foundProduct });
                }
            }

            return catalog;
        }

        private Product FindProduct(Guid id)
        {
            Product product = _products.FirstOrDefault(product => product.ProductName.Id == id);
            if (product == default)
            {
                throw new ProductException("Error: product not found");
            }

            return product;
        }
    }
}