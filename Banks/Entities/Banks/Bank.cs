using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

        public BankConditions BankConditions { get; }
        public Guid Id { get; }
        public string Name { get; }
        private List<Client> Clients { get; }
        private List<IAccount> Accounts { get; }

        public Client AddClient(string firstName, string secondName)
        {
            var client = new Client(firstName, secondName, Id);
            Clients.Add(client);
            return client;
        }

        public Client FindClient(Guid id) => Clients.FirstOrDefault(client => client.Id == id);

        public void Remove(Client client) => Clients.Remove(client);

        public IAccount CreateAccount<T>(Client client, decimal startCash = 0)
        where T : IAccount
        {
            IAccount account = null;

            if (typeof(T) == typeof(CreditAccount))
                account = new CreditAccount(client, this, startCash);
            if (typeof(T) == typeof(DepositAccount))
                account = new DepositAccount(client, this, startCash);
            if (typeof(T) == typeof(DebitAccount))
                account = new DebitAccount(client, this, startCash);

            Accounts.Add(account);
            return account;
        }

        public IAccount FindAccount(Guid id) => Accounts.FirstOrDefault(account => account.Id == id);

        public bool RemoveAccount(IAccount account) => Accounts.Remove(account);

        public List<IAccount> GetAccounts() => new List<IAccount>(Accounts);

        public void MakeMonthlyAddition(DateTime operationDate)
        {
            Accounts.ForEach(account =>
            {
                account.SetCash(account.VirtualCash + account.Cash, Id, operationDate);
                account.SetVirtualCash(0, Id, operationDate);
            });
        }

        public void MakeDailyAddition(DateTime operationDate)
        {
            Accounts.ForEach(acc => MakeDailyAddition(acc, operationDate));
        }

        private void MakeDailyAddition(IAccount account, DateTime operationDate)
        {
            if (account is DebitAccount)
            {
                account.SetVirtualCash(CalculationMoneyForDebitAccount(account.Cash) + account.VirtualCash, Id, operationDate);
            }
            else if (account is DepositAccount)
            {
                decimal deltaCash = 0;
                if (operationDate < account.CreationDate + BankConditions.DepositCondition.Lifetime)
                    deltaCash = CalculationMoneyForDepositAccount(account.Cash);
                else
                    deltaCash = CalculationMoneyForDebitAccount(account.Cash);
                account.SetVirtualCash(deltaCash + account.VirtualCash, Id, operationDate);
            }
            else if (account is CreditAccount)
            {
                account.SetVirtualCash(CalculateMoneyForCreditAccount(account.Cash) + account.VirtualCash, Id, operationDate);
            }
            else
            {
                throw new BankException("Error: can't make daily addition this type");
            }
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