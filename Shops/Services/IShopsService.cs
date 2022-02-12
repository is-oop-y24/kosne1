using Shops.Entities.CatalogKeepers;
using Shops.Entities.Products;
using Shops.Entities.Transfers;

namespace Shops.Services
{
    public interface IShopsService
    {
        Storage AddStorage();
        Shop AddShop(string address, string name, decimal money);
        Customer AddCustomer(string name, decimal money);

        void AddProductsToStorage(Storage storage, Catalog catalog);
        Transfer Supply(Storage storage, Shop shop, Catalog catalog);
        Transfer Purchase(Shop shop, Customer customer, Catalog catalog);

        void ChangePrice(Shop shop, Catalog catalog);

        Shop FindShopWithMinimumProductsValue(Catalog catalog);
    }
}