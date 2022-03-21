using System;
using Banks.Entities.Banks;
using Banks.Tools.Exceptions.SpecificExceptions;

namespace Banks.Entities.Accounts
{
    public class CreditAccount : AbstractAccount
    {
        public CreditAccount(Client client, Bank bank, decimal cash = 0)
            : base(client, bank, cash)
        {
        }

        public override void SetCash(decimal money, Guid bankId, DateTime dateTime = default)
        {
            if (Bank.Id != bankId) throw new AccountException("Error: ID does not match bank ID");
            if (Bank.BankConditions.CreditCondition.Limit > money)
                throw new AccountException("Error: the amount of money is less than the limit balance");
            Cash = money;
        }

        public override void SetVirtualCash(decimal money, Guid bankId, DateTime dateTime = default)
        {
            if (Bank.Id != bankId) throw new AccountException("Error: ID does not match bank ID");
            VirtualCash = money;
        }
    }
}