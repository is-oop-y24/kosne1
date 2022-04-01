using System;
using Banks.ConsoleUI;
using Banks.Entities.Banks;
using Banks.Entities.Transactions;
using Banks.Tools.Exceptions;
using Banks.UI.ConsoleUI.CrudNodes;

namespace Banks.UI.ConsoleUI
{
    public class MainNodeConsoleUI : NodeConsoleUI
    {
        private CrudNodeConsoleUI _crudNode;
        private TransactionNodeConsoleUI _transactionNode;
        private SkipTimeNodeConsoleUI _skipTimeService;
        public MainNodeConsoleUI()
            : base(null, 3)
        {
            _crudNode = new CrudNodeConsoleUI(this);
            _transactionNode = new TransactionNodeConsoleUI(this);
            _skipTimeService = new SkipTimeNodeConsoleUI(this);
        }

        public override void Launch()
        {
            Console.WriteLine(" Menu \n 1.CRUD \n 2.Transactions operation by guid \n 3.Skip Time \n");

            int point = ReadMenuPoint();
            switch (point)
            {
                case 1:
                    _crudNode.Launch();
                    break;
                case 2:
                    TransactionLaunch();
                    break;
                case 3:
                    _skipTimeService.Launch();
                    break;
            }
        }

        private void TransactionLaunch()
        {
            try
            {
                Console.WriteLine("Write transaction guid");
                var transactionId = new Guid(Console.ReadLine() ?? string.Empty);
                ITransaction transaction = CentralBank.Instance.FindTransaction(transactionId);
                _transactionNode.Transaction = transaction;
                _transactionNode.Launch();
            }
            catch (BanksException)
            {
                Console.WriteLine("There is not transaction with this guid");
            }
            catch (FormatException)
            {
                Console.WriteLine("Wrong Guid format");
            }
        }
    }
}