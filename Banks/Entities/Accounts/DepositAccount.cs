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

        public override void SetCash(decimal money, Guid bankId)
        {
            if (Bank.Id != bankId) throw new AccountException("Error: ID does not match bank ID");
            if (DateTime.Now < Bank.BankConditions.DepositCondition.EndTime && Cash <= money)
                throw new AccountException("Error: it is not possible to withdraw money before the time expires");
            Cash = money;
        }

        public override void SetVirtualCash(decimal money, Guid bankId)
        {
            if (Bank.Id != bankId) throw new AccountException("Error: ID does not match bank ID");
            VirtualCash = money;
        }
    }
}