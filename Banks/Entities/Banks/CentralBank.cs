using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Entities.Transactions;
using Banks.Tools.SpecificExceptions;

namespace Banks.Entities.Banks
{
    public class CentralBank
    {
        private static readonly object Padlock = new object();
        private static CentralBank instance;
        private CentralBank()
        {
            Banks = new List<Bank>();
            Transactions = new List<ITransaction>();
        }

        // singleton attempted thread-safety using double-check locking
        public static CentralBank Instance
        {
            get
            {
                if (instance != null) return instance;
                lock (Padlock)
                {
                    instance ??= new CentralBank();
                }

                return instance;
            }
        }

        private List<Bank> Banks { get; }
        private List<ITransaction> Transactions { get; }

        public void AddBank(Bank bank) => Banks.Add(bank);
        public Bank FindBank(Guid id) => Banks.FirstOrDefault(bank => bank.Id == id);
        public void RemoveBank(Bank bank) => Banks.Remove(bank);

        public void TransactionRealize(ITransaction transaction)
        {
            if (transaction.WasRealized) throw new TransactionException("Error: transaction already realized");
            if (transaction.WasCanceled) throw new TransactionException("Error: transaction already canceled");

            if (transaction is Refill)
            {
                var bankTo = FindBank(transaction.BankToId);
                var accountTo = bankTo.FindAccount(transaction.AccountToId);
                if (!transaction.HasAccess(accountTo.Client))
                    throw new TransactionException("Error: transaction failed");
                accountTo.SetCash(accountTo.Cash + transaction.Size, transaction.BankToId, DateTime.Now);
            }
            else if (transaction is Withdrawal)
            {
                var bankFrom = FindBank(transaction.BankFromId);
                var accountFrom = bankFrom.FindAccount(transaction.AccountFromId);
                if (!accountFrom.Client.Confirmation &&
                    transaction.Size > bankFrom.BankConditions.UnconfirmedCondition.MaxTransactionValue)
                    throw new TransactionException("Error: this unconfirmed client can't transfer money");
                if (!transaction.HasAccess(accountFrom.Client))
                    throw new TransactionException("Error: transaction failed");
                accountFrom.SetCash(accountFrom.Cash - transaction.Size, transaction.BankFromId, DateTime.Now);
            }
            else if (transaction is Transaction)
            {
                var bankTo = FindBank(transaction.BankToId);
                var bankFrom = FindBank(transaction.BankFromId);
                var accountTo = bankTo.FindAccount(transaction.AccountToId);
                var accountFrom = bankFrom.FindAccount(transaction.AccountFromId);
                if (!accountFrom.Client.Confirmation &&
                    transaction.Size > bankFrom.BankConditions.UnconfirmedCondition.MaxTransactionValue)
                    throw new TransactionException("Error: this unconfirmed client can't transfer money");
                if (!transaction.HasAccess(accountFrom.Client))
                    throw new TransactionException("Error: transaction failed");
                accountFrom.SetCash(accountFrom.Cash - transaction.Size, transaction.BankFromId, DateTime.Now);
                accountTo.SetCash(accountTo.Cash + transaction.Size, transaction.BankToId, DateTime.Now);
            }

            Transactions.Add(transaction);
            transaction.WasRealized = true;
        }

        public void TransactionCanceled(ITransaction transaction)
        {
            if (!transaction.WasRealized) throw new TransactionException("Error: transaction not realized");
            if (transaction.WasCanceled) throw new TransactionException("Error: transaction already canceled");

            if (transaction is Refill)
            {
                var bankTo = FindBank(transaction.BankToId);
                var accountTo = bankTo.FindAccount(transaction.AccountToId);
                if (!transaction.HasAccess(accountTo.Client))
                    throw new TransactionException("Error: transaction failed");
                accountTo.SetCash(accountTo.Cash - transaction.Size, transaction.BankToId, DateTime.Now);
            }
            else if (transaction is Withdrawal)
            {
                var bankFrom = FindBank(transaction.BankFromId);
                var accountFrom = bankFrom.FindAccount(transaction.AccountFromId);
                if (!transaction.HasAccess(accountFrom.Client))
                    throw new TransactionException("Error: transaction failed");
                accountFrom.SetCash(accountFrom.Cash + transaction.Size, transaction.BankFromId, DateTime.Now);
            }
            else if (transaction is Transaction)
            {
                var bankTo = FindBank(transaction.BankToId);
                var bankFrom = FindBank(transaction.BankFromId);
                var accountTo = bankTo.FindAccount(transaction.AccountToId);
                var accountFrom = bankFrom.FindAccount(transaction.AccountFromId);
                if (!transaction.HasAccess(accountFrom.Client))
                    throw new TransactionException("Error: transaction failed");
                accountFrom.SetCash(accountFrom.Cash + transaction.Size, transaction.BankFromId, DateTime.Now);
                accountTo.SetCash(accountTo.Cash - transaction.Size, transaction.BankToId, DateTime.Now);
            }

            Transactions.Add(transaction);
            transaction.WasCanceled = true;
        }

        public ITransaction FindTransaction(Guid transactionId)
        {
            return Transactions.FirstOrDefault(transaction => transaction.Id == transactionId);
        }

        public void MakeMonthlyAddition(DateTime operationDate)
        {
            Banks.ForEach(b => b.MakeMonthlyAddition(operationDate));
        }

        public void MakeDailyAddition(DateTime operationDate)
        {
            Banks.ForEach(b => b.MakeDailyAddition(operationDate));
        }
    }
}