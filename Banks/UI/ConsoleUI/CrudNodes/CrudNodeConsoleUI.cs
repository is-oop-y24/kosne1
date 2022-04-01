using System;
using Banks.ConsoleUI;
using Banks.ConsoleUI.CrudNodes;

namespace Banks.UI.ConsoleUI.CrudNodes
{
    public class CrudNodeConsoleUI : NodeConsoleUI
    {
        private TransactionCrudNodeConsoleUI _transactionNode;
        private BankCrudNodeConsoleUI _bankNode;
        public CrudNodeConsoleUI(NodeConsoleUI parentNode)
            : base(parentNode, 4)
        {
            _transactionNode = new TransactionCrudNodeConsoleUI(this);
            _bankNode = new BankCrudNodeConsoleUI(this);
        }

        public override void Launch()
        {
            while (true)
            {
                Console.WriteLine(" Menu \n 1.Transaction Crud \n 2.Bank Crud \n 3.Exit \n");
                int point = ReadMenuPoint();
                switch (point)
                {
                    case 1:
                        _transactionNode.Launch();
                        break;
                    case 2:
                        _bankNode.Launch();
                        break;
                    case 3:
                        Exit();
                        break;
                }
            }
        }
    }
}