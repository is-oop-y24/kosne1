using System;
using Banks.Entities.Banks;

namespace Banks.Entities.Transactions
{
    public class Withdrawal : AbstractTransaction
    {
        public Withdrawal(decimal size, Guid bankId, Guid accountId)
            : base(size)
        {
            BankFromId = bankId;
            AccountFromId = accountId;
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