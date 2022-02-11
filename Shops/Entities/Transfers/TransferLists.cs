using System.Collections.Generic;

namespace Shops.Entities.Transfers
{
    public class TransferLists
    {
        private List<Transfer> _supplyLists;
        private List<Transfer> _purchaseLists;

        public TransferLists()
        {
            _supplyLists = new List<Transfer>();
            _purchaseLists = new List<Transfer>();
        }

        public List<Transfer> GetSupplyList()
        {
            return new List<Transfer>(_supplyLists);
        }

        public List<Transfer> GetPurchaseList()
        {
            return new List<Transfer>(_purchaseLists);
        }

        public void AddSupply(Transfer transfer)
        {
            _supplyLists.Add(transfer);
        }

        public void AddPurchase(Transfer transfer)
        {
            _purchaseLists.Add(transfer);
        }
    }
}