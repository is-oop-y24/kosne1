using System;
using Banks.Entities;
using Banks.Entities.Accounts;
using Banks.Entities.Banks;
using Banks.Entities.Banks.Conditions;
using Banks.Entities.ClientInformationStrategies;
using Banks.Entities.Transactions;
using NUnit.Framework;

namespace Banks.Tests
{
    public class BanksTests
    {
        private CentralBank centralBank;

        [SetUp]
        public void Setup()
        {
            centralBank = CentralBank.Instance;
        }

        [Test]
        public void CreateBankTest()
        {
            var bankConditions = new BankConditions(UnconfirmedCondition.DefaultValue(), DebitCondition.DefaultValue(),
                DepositCondition.DefaultValue(), CreditCondition.DefaultValue());
            var sberbank = new Bank("Sberbank", bankConditions);
            centralBank.AddBank(sberbank);
            Bank foundBank = centralBank.FindBank(sberbank.Id);
            Assert.IsTrue(sberbank.Name == foundBank.Name);
        }
        
        [Test]
        public void CheckDepositRulesAlgorithm()
        {

            var depositCondition = new DepositCondition(0.01, 100, TimeSpan.Zero);
            Assert.AreEqual(depositCondition.GetInterest(50), 0.005);
        }

        [Test]
        public void CheckBankRuleSubscriptionSystem()
        {
            var bankConditions = new BankConditions(UnconfirmedCondition.DefaultValue(), DebitCondition.DefaultValue(),
                DepositCondition.DefaultValue(), CreditCondition.DefaultValue());
            var sberbank = new Bank("Sberbank", bankConditions);
            centralBank.AddBank(sberbank);
            Client client = sberbank.AddClient("Nikolai", "Ananin");
            sberbank.BankConditions.Listen(DebitCondition.ConditionName, client);

            var testInformationStrategy = new TestInformationStrategy();
            
            client.InformationStrategy = testInformationStrategy;
            sberbank.BankConditions.DebitCondition = new DebitCondition(100);
            
            Assert.AreEqual(testInformationStrategy.LastNotification, client.FullName);
        }

        [Test]
        public void CheckRefillRealization()
        {
            var bankConditions = new BankConditions(UnconfirmedCondition.DefaultValue(), DebitCondition.DefaultValue(),
                DepositCondition.DefaultValue(), CreditCondition.DefaultValue());
            var sberbank = new Bank("Sberbank", bankConditions);
            centralBank.AddBank(sberbank);
            Client client = sberbank.AddClient("Nikolai", "Ananin");
            IAccount account = sberbank.CreateAccount<DebitAccount>(client);
            
            centralBank.TransactionRealize(new Refill(100, sberbank.Id, account.Id));
            Assert.AreEqual(account.Cash, 100);
        }

        [Test]
        public void CheckTransactionRealization()
        {
            var bankConditions = new BankConditions(UnconfirmedCondition.DefaultValue(), DebitCondition.DefaultValue(),
                DepositCondition.DefaultValue(), CreditCondition.DefaultValue());
            var sberbank = new Bank("Sberbank", bankConditions);
            centralBank.AddBank(sberbank);
            Client client = sberbank.AddClient("Nikolai", "Ananin");
            IAccount account1 = sberbank.CreateAccount<DebitAccount>(client, 100);
            IAccount account2 = sberbank.CreateAccount<CreditAccount>(client);
            client.AddAddress("a");
            client.AddPassportNumber("122q4234");
            
            centralBank.TransactionRealize(
                new Transaction(100, sberbank.Id, account1.Id, 
                    sberbank.Id, account2.Id));
            Assert.AreEqual(account1.Cash, 0);
            Assert.AreEqual(account2.Cash, 100);
        }
    }
}