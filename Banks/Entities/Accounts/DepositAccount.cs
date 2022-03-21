using System;
using Banks.Entities.Banks;
using Banks.Tools.Exceptions.SpecificExceptions;
using Banks.Tools.SpecificExceptions;

namespace Banks.Entities.Accounts
{
    public class DepositAccount : AbstractAccount
    {
        public DepositAccount(Client client, Bank bank, decimal cash = 0)
            : base(client, bank, cash)
        {
        }

        public override void SetCash(decimal money, Guid bankId, DateTime dateTime = default)
        {
            if (dateTime == default) dateTime = DateTime.Now;
            if (Bank.Id != bankId) throw new AccountException("Error: ID does not match bank ID");
            if (dateTime < CreationDate + Bank.BankConditions.DepositCondition.Lifetime && Cash >= money)
                throw new AccountException("Error: it is not possible to withdraw money before the time expires");
            Cash = money;
        }

        public override void SetVirtualCash(decimal money, Guid bankId, DateTime dateTime = default)
        {
            if (Bank.Id != bankId) throw new AccountException("Error: ID does not match bank ID");
            VirtualCash = money;
        }
    }
}