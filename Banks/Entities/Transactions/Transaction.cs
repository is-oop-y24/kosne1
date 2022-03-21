using System;
using Banks.Entities.Banks;

namespace Banks.Entities.Transactions
{
    public class Transaction : AbstractTransaction
    {
        public Transaction(decimal size, Guid bankFromId, Guid accountFromId, Guid bankToId, Guid accountToId)
            : base(size)
        {
            BankFromId = bankFromId;
            AccountFromId = accountFromId;
            BankToId = bankToId;
            AccountToId = accountToId;
        }

        public override bool HasAccess(Client client)
        {
            if (client.BankId != BankFromId) return false;

            var bank = CentralBank.Instance.FindBank(BankFromId);
            if (bank == default) return false;
            var account = bank.FindAccount(AccountFromId);
            if (account == default) return false;
            var realClient = account.Client;

            return realClient.Id == client.Id;
        }
    }
}