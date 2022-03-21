using System;
using Banks.Exceptions;
using Banks.Interfaces;

namespace Banks.ConsoleUI
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
            if (Transaction.IsRealized)
            {
                Console.WriteLine("This transaction was already realized");
                return;
            }

            try
            {
                Transaction.Realize();
                Console.WriteLine("Transaction was successfully realized");
            }
            catch (BankSystemException e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Transaction data is wrong");
            }
        }

        private void CancelLaunch()
        {
            if (Transaction.IsCanceled)
            {
                Console.WriteLine("This transaction was already canceled");
                return;
            }

            try
            {
                Transaction.Cancel();
                Console.WriteLine("Transaction was successfully canceled");
            }
            catch (BankSystemException e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Transaction data is wrong");
            }
        }
    }
}