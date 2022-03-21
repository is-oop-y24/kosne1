using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Entities.Transactions;

namespace Banks.Entities.Banks
{
    public class CentralBank
    {
        private static readonly object Padlock = new object();
        private static CentralBank instance;
        private CentralBank()
        {
            Banks = new List<Bank>();
            Transactions = new List<ITransaction>();
        }

        private List<Bank> Banks { get; }
        private List<ITransaction> Transactions { get; }

        // singleton attempted thread-safety using double-check locking
        public static CentralBank Instance
        {
            get
            {
                if (instance != null) return instance;
                lock (Padlock)
                {
                    instance ??= new CentralBank();
                }

                return instance;
            }
        }

        public void AddBank(Bank bank) => Banks.Add(bank);
        public Bank FindBank(Guid id) => Banks.FirstOrDefault(bank => bank.Id == id);
        public void RemoveBank(Bank bank) => Banks.Remove(bank);
    }
}