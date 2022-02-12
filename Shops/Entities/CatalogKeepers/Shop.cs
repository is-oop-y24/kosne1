using System;
using Shops.Entities.Products;
using Shops.Entities.Transfers;

namespace Shops.Entities.CatalogKeepers
{
    public class Shop : IKeeper
    {
        public Shop(string address, string name, decimal money)
        {
            Id = Guid.NewGuid();
            Address = address;
            Name = name;
            Money = money;
            Catalog = new Catalog();
            TransferLists = new TransferLists();
        }

        public string Name { get; }
        public Guid Id { get; }
        public string Address { get; }
        public decimal Money { get; set; }
        public Catalog Catalog { get; }
        public TransferLists TransferLists { get; }
    }
}