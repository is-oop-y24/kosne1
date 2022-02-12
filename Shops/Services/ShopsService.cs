using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Entities.CatalogKeepers;
using Shops.Entities.Products;
using Shops.Entities.Transfers;
using Shops.Tools.SpecificExceptions;

namespace Shops.Services
{
    public class ShopsService : IShopsService
    {
        private List<Shop> _shops;
        public ShopsService()
        {
            _shops = new List<Shop>();
        }

        public Storage AddStorage()
        {
            var storage = new Storage();
            return storage;
        }

        public Shop AddShop(string address, string name, decimal money)
        {
            var shop = new Shop(address, name, money);
            _shops.Add(shop);
            return shop;
        }

        public Customer AddCustomer(string name, decimal money)
        {
            var customer = new Customer(name, money);
            return customer;
        }

        public void AddProductsToStorage(Storage storage, Catalog catalog)
        {
            storage.Catalog.AddProducts(catalog.GetProducts());
        }

        public Transfer Supply(Storage storage, Shop shop, Catalog catalog)
        {
            var transfer = new Transfer(storage, shop, catalog);
            storage.TransferLists.AddSupply(transfer);
            shop.TransferLists.AddSupply(transfer);
            return transfer;
        }

        public Transfer Purchase(Shop shop, Customer customer, Catalog catalog)
        {
            var transfer = new Transfer(shop, customer, catalog);
            shop.TransferLists.AddPurchase(transfer);
            shop.TransferLists.AddPurchase(transfer);
            return transfer;
        }

        public void ChangePrice(Shop shop, Catalog catalog)
        {
            shop.Catalog.ChangePrice(catalog.GetProducts());
        }

        public Shop FindShopWithMinimumProductsValue(Catalog catalog)
        {
            decimal totalCost = decimal.MaxValue;
            Guid id = default;
            foreach (Shop foundShop in _shops)
            {
                Catalog newCatalog = foundShop.Catalog.ProductsForMinimumProductsValue(catalog.GetProducts());
                decimal tempTotalCoast = newCatalog.TotalCost();
                if (tempTotalCoast >= totalCost) continue;
                totalCost = tempTotalCoast;
                id = foundShop.Id;
            }

            Shop shop = _shops.First(shop => shop.Id == id);

            if (!shop.Catalog.HaveProducts(catalog.GetProducts()))
            {
                throw new ProductException("Error: not enough products");
            }

            return shop;
        }
    }
}