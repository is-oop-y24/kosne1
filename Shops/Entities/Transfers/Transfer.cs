using System;
using Shops.Entities.CatalogKeepers;
using Shops.Entities.Products;
using Shops.Tools.SpecificExceptions;

namespace Shops.Entities.Transfers
{
    public class Transfer
    {
        public Transfer(IKeeper from, IKeeper to, Catalog catalog)
        {
            Catalog newCatalog = from.Catalog.Receipt(catalog.GetProducts());
            if (!CanRealize(from, to, newCatalog))
            {
                throw new TransferException("Error: transfer cannot be made");
            }

            Realize(from, to, newCatalog);

            From = from;
            To = to;
            Catalog = newCatalog;
        }

        public IKeeper From { get; }
        public IKeeper To { get; }
        public Catalog Catalog { get; }

        private bool CanRealize(IKeeper from, IKeeper to, Catalog catalog)
        {
            if (!from.Catalog.HaveProducts(catalog.GetProducts()))
            {
                throw new ProductException("Error: not enough products");
            }

            if (to.Money < catalog.TotalCost())
            {
                throw new MoneyException("Error: not enough money");
            }

            return true;
        }

        private void Realize(IKeeper from, IKeeper to, Catalog catalog)
        {
            decimal money = catalog.TotalCost();
            from.Money += catalog.TotalCost();
            to.Money -= catalog.TotalCost();
            to.Catalog.AddProducts(catalog.GetProducts());
            from.Catalog.RemoveProducts(catalog.GetProducts());
        }
    }
}