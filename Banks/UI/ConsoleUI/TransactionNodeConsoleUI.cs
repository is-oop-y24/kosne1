using System;
using Banks.Entities.Banks;
using Banks.Entities.Transactions;
using Banks.Tools.Exceptions;

namespace Banks.UI.ConsoleUI
{
    public class TransactionNodeConsoleUI : NodeConsoleUI
    {
        public TransactionNodeConsoleUI(NodeConsoleUI parentNode)
            : base(parentNode, 3)
        {
        }

        public ITransaction Transaction { get; set; }

        public override void Launch()
        {
            while (true)
            {
                Console.WriteLine(" Menu \n 1.Realize \n 2.Cancel \n 3.Exit \n");

                int point = ReadMenuPoint();
                switch (point)
                {
                    case 1:
                        RealizeLaunch();
                        break;
                    case 2:
                        CancelLaunch();
                        break;
                    case 3:
                        Exit();
                        break;
                }
            }
        }

        private void RealizeLaunch()
        {
            if (Transaction.WasRealized)
            {
                Console.WriteLine("This transaction was already realized");
                return;
            }

            try
            {
                CentralBank.Instance.TransactionRealize(Transaction);
                Console.WriteLine("Transaction was successfully realized");
            }
            catch (BanksException e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Transaction data is wrong");
            }
        }

        private void CancelLaunch()
        {
            if (Transaction.WasCanceled)
            {
                Console.WriteLine("This transaction was already canceled");
                return;
            }

            try
            {
                CentralBank.Instance.TransactionCanceled(Transaction);
                Console.WriteLine("Transaction was successfully canceled");
            }
            catch (BanksException e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Transaction data is wrong");
            }
        }
    }
}