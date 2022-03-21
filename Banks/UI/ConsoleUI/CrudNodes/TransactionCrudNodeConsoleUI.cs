using System;
using Banks.Entities.Banks;
using Banks.Entities.Transactions;
using Banks.UI.ConsoleUI;
using Newtonsoft.Json;

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
                        Console.WriteLine(JsonConvert.SerializeObject(CentralBank.Instance.GetTransactions()));
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
                            var bankId = new Guid(Console.ReadLine() ?? string.Empty);
                            var accountId = new Guid(Console.ReadLine() ?? string.Empty);
                            decimal value = Convert.ToDecimal(Console.ReadLine());
                            CentralBank.Instance.CreateTransaction(new Refill(value, bankId, accountId));
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
                            var bankFromId = new Guid(Console.ReadLine() ?? string.Empty);
                            var accountFromId = new Guid(Console.ReadLine() ?? string.Empty);
                            var bankToId = new Guid(Console.ReadLine() ?? string.Empty);
                            var accountToId = new Guid(Console.ReadLine() ?? string.Empty);
                            decimal value = Convert.ToDecimal(Console.ReadLine());
                            CentralBank.Instance.CreateTransaction(new Transaction(value, bankFromId, accountFromId, bankToId, accountToId));
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
                            var bankId = new Guid(Console.ReadLine() ?? string.Empty);
                            var accountId = new Guid(Console.ReadLine() ?? string.Empty);
                            decimal value = Convert.ToDecimal(Console.ReadLine());
                            CentralBank.Instance.CreateTransaction(new Withdrawal(value, bankId, accountId));
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