using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Entities.Accounts;
using Banks.Entities.Banks.Conditions;
using Banks.Tools.SpecificExceptions;

namespace Banks.Entities.Banks
{
    public class Bank
    {
        public Bank(string name, BankConditions bankConditions)
        {
            if (name == string.Empty) throw new BankException("Error: empty bank name entered");

            Id = Guid.NewGuid();
            Name = name;
            Clients = new List<Client>();
            Accounts = new List<IAccount>();
            BankConditions = bankConditions;
        }

        public Guid Id { get; }
        public string Name { get; }
        private List<Client> Clients { get; }
        private List<IAccount> Accounts { get; }
        public BankConditions BankConditions { get; }

        public Client AddClient(string firstName, string secondName)
        {
            var client = new Client(firstName, secondName, Id);
            Clients.Add(client);
            return client;
        }

        public Client FindClient(Guid id) => Clients.FirstOrDefault(client => client.Id == id);

        public void Remove(Client client) => Clients.Remove(client);

        public IAccount CreateAccount<T>(Client client)
        where T : IAccount
        {
            IAccount account = null;

            if (typeof(T) == typeof(CreditAccount))
                account = new CreditAccount(client, this);
            if (typeof(T) == typeof(DepositAccount))
                account = new DepositAccount(client, this);
            if (typeof(T) == typeof(DebitAccount))
                account = new DebitAccount(client, this);

            Accounts.Add(account);
            return account;
        }

        public IAccount FindAccount(Guid id) => Accounts.FirstOrDefault(account => account.Id == id);

        public void RemoveAccount(IAccount account) => Accounts.Remove(account);

        public void MakeMonthAddition()
        {
            Accounts.ForEach(account =>
            {
                account.SetCash(account.VirtualCash + account.Cash, Id);
                account.SetVirtualCash(0, Id);
            });
        }

        public void MakeDailyAddition()
        {
            Accounts.ForEach(account => MakeDailyAddition(account));
        }

        private void MakeDailyAddition(IAccount account)
        {
            if (account is DebitAccount)
                account.SetVirtualCash(CalculationMoneyForDebitAccount(account.Cash) + account.VirtualCash, Id);
            else if (account is DepositAccount)
                account.SetVirtualCash(CalculationMoneyForDepositAccount(account.Cash) + account.VirtualCash, Id);
            else if (account is CreditAccount)
                account.SetVirtualCash(CalculateMoneyForCreditAccount(account.Cash) + account.VirtualCash, Id);
            else throw new BankException("Error: can't make daily addition this type");
        }

        private decimal CalculationMoneyForDepositAccount(decimal money)
        {
            return money * (decimal)BankConditions.DepositCondition.GetInterest(money);
        }

        private decimal CalculationMoneyForDebitAccount(decimal money)
        {
            return money * (decimal)BankConditions.DebitCondition.CashbackInterest;
        }

        private decimal CalculateMoneyForCreditAccount(decimal money)
        {
            if (money < 0) return -BankConditions.CreditCondition.FixCommission;
            return 0;
        }
    }
}