using System;
using Shops.Entities.Products;
using Shops.Entities.Transfers;

namespace Shops.Entities.CatalogKeepers
{
    public class Customer : IKeeper
    {
        public Customer(string name, decimal money)
        {
            Id = Guid.NewGuid();
            Name = name;
            Money = money;
            Catalog = new Catalog();
            TransferLists = new TransferLists();
        }

        public string Name { get; }
        public decimal Money { get; set; }
        public Guid Id { get; }
        public Catalog Catalog { get; }
        public TransferLists TransferLists { get; }
    }
}