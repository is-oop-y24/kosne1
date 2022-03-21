using System;
using Banks.Entities.Banks;

namespace Banks.Entities.Accounts
{
    public abstract class AbstractAccount : IAccount
    {
        public AbstractAccount(Client client, Bank bank, decimal cash = 0)
        {
            Id = Guid.NewGuid();
            Client = client;
            this.Bank = bank;
            Cash = cash;
            VirtualCash = 0;
            CreationDate = DateTime.Now;
        }

        protected Bank Bank { get; }
        public Guid Id { get; }
        public Client Client { get; }
        public decimal Cash { get; protected set; }
        public decimal VirtualCash { get; protected set; }
        public DateTime CreationDate { get; }

        public abstract void SetCash(decimal money, Guid bankId);
        public abstract void SetVirtualCash(decimal money, Guid bankId);
    }
}