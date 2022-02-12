using System.Collections.Generic;
using NUnit.Framework;
using Shops.Entities.CatalogKeepers;
using Shops.Entities.Products;
using Shops.Services;
using Shops.Tools;


namespace Shops.Tests
{
    public class Tests
    {
        private IShopsService _shopsService;

        [SetUp]
        public void Setup()
        {
            _shopsService = new ShopsService();
        }

        [Test]
        public void SupplyOfProductsToShop()
        {
            Storage storage = _shopsService.AddStorage();
            Shop shop = _shopsService.AddShop("Лермонтовский пр., 50, Санкт-Петербург","Дикси", 1000000000);
            var product1 = new Product(new ProductName("белый хлеб"), 50, 10000);
            var product2 = new Product(new ProductName("черный хлеб"), 40, 9999);
            var products = new List<Product> {product1, product2};
            var catalog = new Catalog();
            catalog.AddProducts(products);
            _shopsService.AddProductsToStorage(storage, catalog);
            _shopsService.Supply(storage, shop, catalog);
        }

        [Test]
        public void SettingAndChangingPricesForSomeProductInShop_PricesChanged()
        {
            Storage storage = _shopsService.AddStorage();
            Shop shop = _shopsService.AddShop("Лермонтовский пр., 50, Санкт-Петербург","Дикси", 100000000);
            var product1 = new Product(new ProductName("белый хлеб"), 50, 10000);
            var product2 = new Product(new ProductName("черный хлеб"), 40, 10000);
            var products = new List<Product> {product1, product2};
            var catalog = new Catalog();
            catalog.AddProducts(products);
            _shopsService.AddProductsToStorage(storage, catalog);
            _shopsService.Supply(storage, shop, catalog);

            product1.Price *= (decimal) 1.2;
            product2.Price *= (decimal) 1.2;
            var products1 = new List<Product> {product1, product2};
            var catalog1 = new Catalog();
            catalog1.AddProducts(products1);
            
            _shopsService.ChangePrice(shop, catalog1);
            if (catalog.TotalCost() < catalog1.TotalCost())
            {
                Assert.Fail();
            }
        }

        [Test]
        public void SearchShopWithCheapestProducts_ProductsFound()
        {
            Storage storage = _shopsService.AddStorage();
            var productName1 = new ProductName("белый хлеб");
            var productName2 = new ProductName("черный хлеб");
            
            var product1 = new Product(productName1, 50, 10000);
            var product2 = new Product(productName2, 40, 10000);
            var products1_0 = new List<Product> {product1, product2};
            var catalog1_0 = new Catalog();
            catalog1_0.AddProducts(products1_0);
            
            product1 = new Product(productName1, 50, 2000);
            product2 = new Product(productName2, 40, 2000);
            var products1_1 = new List<Product> {product1, product2};
            var catalog1_1 = new Catalog();
            catalog1_1.AddProducts(products1_1);
            
            _shopsService.AddProductsToStorage(storage, catalog1_0);
            Shop shop1 = _shopsService.AddShop("Лермонтовский пр., 50, Санкт-Петербург","Дикси", 100000000);
            _shopsService.Supply(storage, shop1, catalog1_1);
            
            product1 = new Product(productName1, 60, 2000);
            product2 = new Product(productName2, 50, 2000);
            var products2_0 = new List<Product> {product1, product2};
            var catalog2_0 = new Catalog();
            catalog2_0.AddProducts(products2_0);
            
            Shop shop2 = _shopsService.AddShop("Лермонтовский пр., 51, Санкт-Петербург","Магнит", 100000000);
            _shopsService.Supply(storage, shop2, catalog2_0);
            
            product1 = new Product(productName1, 10, 0);
            product2 = new Product(productName2, 50, 0);
            var products2_1 = new List<Product> {product1, product2};
            var catalog2_1 = new Catalog();
            catalog2_1.AddProducts(products2_1);
            _shopsService.ChangePrice(shop2, catalog2_1);
            
            product1 = new Product(productName1, 60, 10);
            product2 = new Product(productName2, 50, 10);
            var products5 = new List<Product> {product1, product2};
            var catalog5 = new Catalog();
            catalog5.AddProducts(products5);
            
            Shop foundShop = _shopsService.FindShopWithMinimumProductsValue(catalog5);

            if (foundShop.Name != shop2.Name)
            {
                Assert.Fail();
            }
        }
        
        [Test]
        public void SearchShopWithCheapestProducts_NotEnoughProducts()
        {
            Storage storage = _shopsService.AddStorage();
            
            var productName1 = new ProductName("белый хлеб");
            var productName2 = new ProductName("черный хлеб");
            var product1 = new Product(productName1, 50, 10000);
            var product2 = new Product(productName2, 40, 10000);

            var storageProductList = new List<Product>() {product1, product2};
            var storageCatalog = new Catalog();
            storageCatalog.AddProducts(storageProductList);
            
            _shopsService.AddProductsToStorage(storage, storageCatalog);
            

            Shop shop1 = _shopsService.AddShop("Лермонтовский пр., 50, Санкт-Петербург", "Дикси", 100000000);
            
            product1 = new Product(productName1, 50, 2000);
            product2 = new Product(productName2, 40, 2000);
            
            var shop1ProductList = new List<Product>() {product1, product2};
            var shop1Catalog = new Catalog();
            shop1Catalog.AddProducts(shop1ProductList);

            _shopsService.Supply(storage, shop1, shop1Catalog);
            
            
            Shop shop2 = _shopsService.AddShop("Лермонтовский пр., 51, Санкт-Петербург", "Магнит", 100000000);
            
            product1 = new Product(productName1, 50, 2000);
            product2 = new Product(productName2, 40, 2000);
            
            var shop2ProductList = new List<Product>() {product1, product2};
            var shop2Catalog = new Catalog();
            shop2Catalog.AddProducts(shop2ProductList);
            
            _shopsService.Supply(storage, shop2, shop2Catalog);
            
            
            product1 = new Product(productName1, 40, 0);
            product2 = new Product(productName2, 30, 0);
            
            var shop2ProductListNewPrice = new List<Product>() {product1, product2};
            var shop2CatalogNewPrice = new Catalog();
            shop2CatalogNewPrice.AddProducts(shop2ProductListNewPrice);
            
            _shopsService.ChangePrice(shop2, shop2CatalogNewPrice);
            
            
            product1 = new Product(productName1, 0, 4000);
            product2 = new Product(productName2, 0, 4000);
            
            var searchProductList = new List<Product>() {product1, product2};
            var searchCatalog = new Catalog();
            searchCatalog.AddProducts(searchProductList);

            Assert.Catch<ShopsException>(() =>
            {
                Shop foundShop = _shopsService.FindShopWithMinimumProductsValue(searchCatalog);
            });
        }
        
        [Test]
        public void Purchase_Success()
        {
            Storage storage = _shopsService.AddStorage();
            
            var productName1 = new ProductName("белый хлеб");
            var productName2 = new ProductName("черный хлеб");
            var product1 = new Product(productName1, 50, 10000);
            var product2 = new Product(productName2, 40, 10000);

            var storageProductList = new List<Product>() {product1, product2};
            var storageCatalog = new Catalog();
            storageCatalog.AddProducts(storageProductList);
            
            _shopsService.AddProductsToStorage(storage, storageCatalog);
            

            Shop shop1 = _shopsService.AddShop("Лермонтовский пр., 50, Санкт-Петербург", "Дикси", 1000000);
            
            product1 = new Product(productName1, 50, 200);
            product2 = new Product(productName2, 40, 200);
            
            var shop1ProductList = new List<Product>() {product1, product2};
            var shop1Catalog = new Catalog();
            shop1Catalog.AddProducts(shop1ProductList);

            _shopsService.Supply(storage, shop1, shop1Catalog);

            Customer customer1 = _shopsService.AddCustomer("Ананьин Николай", 1000);
            
            product1 = new Product(productName1, 0, 2);
            product2 = new Product(productName2, 0, 1);
            
            var customer1ProductList = new List<Product>() {product1, product2};
            var customer1Catalog = new Catalog();
            customer1Catalog.AddProducts(customer1ProductList);

            _shopsService.Purchase(shop1, customer1, customer1Catalog);

            if (customer1.Money != 860)
            {
                Assert.Fail();
            }
            
            product1 = new Product(productName1, 0, 2);
            product2 = new Product(productName2, 0, 1);
            
            var productList = new List<Product>() {product1, product2};
            var catalog = new Catalog();
            catalog.AddProducts(productList);
            
            if (!customer1.Catalog.HaveProducts(catalog.GetProducts()))
            {
                Assert.Fail();
            }
        }
    }
}