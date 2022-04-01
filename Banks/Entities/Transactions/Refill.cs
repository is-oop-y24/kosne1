using System;
using Banks.Entities.Banks;

namespace Banks.Entities.Transactions
{
    public class Refill : AbstractTransaction
    {
        public Refill(decimal size, Guid bankId, Guid accountId)
            : base(size)
        {
            BankToId = bankId;
            AccountToId = accountId;
        }

        public override bool HasAccess(Client client)
        {
            if (client.BankId != BankToId) return false;

            var bank = CentralBank.Instance.FindBank(BankToId);
            if (bank == default) return false;
            var account = bank.FindAccount(AccountToId);
            if (account == default) return false;
            var realClient = account.Client;

            return realClient.Id == client.Id;
        }
    }
}