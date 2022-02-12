using Shops.Entities.Products;
using Shops.Entities.Transfers;

namespace Shops.Entities.CatalogKeepers
{
    public interface IKeeper
    {
        public decimal Money { get; set; }
        public Catalog Catalog { get; }
        public TransferLists TransferLists { get; }
    }
}