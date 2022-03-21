using System;
using Banks.Models;
using Banks.Models.Transactions;
using Banks.Tools;

namespace Banks.ConsoleUI.CrudNodes
{
    public class TransactionCrudNodeConsoleUI : NodeConsoleUI
    {
        public TransactionCrudNodeConsoleUI(NodeConsoleUI parentNode)
            : base(parentNode, 4)
        {
        }

        public override void Launch()
        {
            while (true)
            {
                Console.WriteLine(" Menu \n 1.Create \n 2.Read \n 3.Exit \n");
                int point = ReadMenuPoint();
                switch (point)
                {
                    case 1:
                        LaunchCreate();
                        break;
                    case 2:
                        Console.WriteLine(ObjectDumper.Dump(CentralBank.GetInstance().GetTransactions()));
                        break;
                    case 3:
                        Exit();
                        break;
                }
            }
        }

        private void LaunchCreate()
        {
            try
            {
                Console.WriteLine(" 1.Create refill \n 2.Create Transaction \n 3.Create Withdrawal \n");
                int createPoint = ReadMenuPoint(3);
                switch (createPoint)
                {
                    case 1:
                        Console.WriteLine("Write bank guid, account guid, refill size");
                        try
                        {
                            var bankGuid = new Guid(Console.ReadLine() ?? string.Empty);
                            var accountGuid = new Guid(Console.ReadLine() ?? string.Empty);
                            decimal value = Convert.ToDecimal(Console.ReadLine());
                            CentralBank.GetInstance().AddTransaction(new Refill(bankGuid, accountGuid, value));
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine(e);
                        }

                        break;
                    case 2:
                        Console.WriteLine("Write bank from guid, account from guid, bank to guid, account to guid, refill size");
                        try
                        {
                            var bankFromGuid = new Guid(Console.ReadLine() ?? string.Empty);
                            var accountFromGuid = new Guid(Console.ReadLine() ?? string.Empty);
                            var bankToGuid = new Guid(Console.ReadLine() ?? string.Empty);
                            var accountToGuid = new Guid(Console.ReadLine() ?? string.Empty);
                            decimal value = Convert.ToDecimal(Console.ReadLine());
                            CentralBank.GetInstance().AddTransaction(new Transaction(bankFromGuid, accountFromGuid, bankToGuid, accountToGuid, value));
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine(e);
                        }

                        break;
                    case 3:
                        Console.WriteLine("Write bank guid, account guid, refill size");
                        try
                        {
                            var bankGuid = new Guid(Console.ReadLine() ?? string.Empty);
                            var accountGuid = new Guid(Console.ReadLine() ?? string.Empty);
                            decimal value = Convert.ToDecimal(Console.ReadLine());
                            CentralBank.GetInstance().AddTransaction(new Withdrawal(bankGuid, accountGuid, value));
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine(e);
                        }

                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("You make a mistake with inputting data");
            }
        }
    }
}