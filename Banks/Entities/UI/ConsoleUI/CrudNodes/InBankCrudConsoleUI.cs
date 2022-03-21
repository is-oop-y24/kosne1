using System;
using Banks.Models;

namespace Banks.ConsoleUI.CrudNodes
{
    public class InBankCrudConsoleUI : NodeConsoleUI
    {
        private AccountCrudNodeConsoleUI _accountCrudNode;
        private ClientCrudNodeConsoleUI _clientCrudNode;
        public InBankCrudConsoleUI(NodeConsoleUI parentNode)
            : base(parentNode, 3)
        {
            _accountCrudNode = new AccountCrudNodeConsoleUI(this);
            _clientCrudNode = new ClientCrudNodeConsoleUI(this);
        }

        public Bank Bank { get; set; }

        public override void Launch()
        {
            while (true)
            {
                Console.WriteLine(" Menu \n 1.Account Crud \n 2.Client Crud \n 3.Exit \n");
                int point = ReadMenuPoint();
                switch (point)
                {
                    case 1:
                        _accountCrudNode.Bank = Bank;
                        _accountCrudNode.Launch();
                        break;
                    case 2:
                        _clientCrudNode.Bank = Bank;
                        _clientCrudNode.Launch();
                        break;
                    case 3:
                        Exit();
                        break;
                }
            }
        }
    }
}