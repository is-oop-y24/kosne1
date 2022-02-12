using Shops.Entities.Products;
using Shops.Entities.Transfers;

namespace Shops.Entities.CatalogKeepers
{
    public class Storage : IKeeper
    {
        public Storage()
        {
            Money = default;
            Catalog = new Catalog();
            TransferLists = new TransferLists();
        }

        public decimal Money { get; set; }
        public Catalog Catalog { get; }
        public TransferLists TransferLists { get; }
    }
}