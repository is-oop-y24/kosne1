using System;
using Banks.Entities.Banks;
using Banks.Tools.Exceptions.SpecificExceptions;
using Banks.Tools.SpecificExceptions;

namespace Banks.Entities.Accounts
{
    public class DebitAccount : AbstractAccount
    {
        public DebitAccount(Client client, Bank bank, decimal cash = 0)
            : base(client, bank, cash)
        {
        }

        public override void SetCash(decimal money, Guid bankId)
        {
            if (Bank.Id != bankId) throw new AccountException("Error: ID does not match bank ID");
            if (money < 0) throw new AccountException("Error: invalid money value");
            Cash = money;
        }

        public override void SetVirtualCash(decimal money, Guid bankId)
        {
            if (Bank.Id != bankId) throw new AccountException("Error: ID does not match bank ID");
            if (money < 0) throw new AccountException("Error: invalid money value");
            VirtualCash = money;
        }
    }
}