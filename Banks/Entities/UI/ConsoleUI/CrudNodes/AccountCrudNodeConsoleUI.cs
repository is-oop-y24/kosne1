using System;
using System.Reflection.Metadata;
using Banks.Entities;
using Banks.Entities.Accounts;
using Banks.Entities.Banks;

namespace Banks.ConsoleUI.CrudNodes
{
    public class AccountCrudNodeConsoleUI : NodeConsoleUI
    {
        public AccountCrudNodeConsoleUI(NodeConsoleUI parentNode)
            : base(parentNode, 4)
        {
        }

        public Bank Bank { get; set; }

        public override void Launch()
        {
            while (true)
            {
                Console.WriteLine(" Menu \n 1.Create account \n 2.Read accounts \n 3.Remove account \n 4.Exit \n");
                int point = ReadMenuPoint();
                switch (point)
                {
                    case 1:
                        CreateLaunch();
                        break;
                    case 2:
                        ReadLaunch();
                        break;
                    case 3:
                        RemoveLaunch();
                        break;
                    case 4:
                        Exit();
                        break;
                }
            }
        }

        public void CreateLaunch()
        {
            try
            {
                Console.WriteLine("Write client guid");
                var clientGuid = new Guid(Console.ReadLine() ?? string.Empty);
                Client client = Bank.FindClient(clientGuid);

                Console.WriteLine("Choose account type 1.Debit 2.Credit 3.Deposit");
                int type = ReadMenuPoint(3);

                Console.WriteLine("Write start balance");
                decimal balance = Convert.ToDecimal(Console.ReadLine());

                IAccount account = null;
                switch (type)
                {
                    case 1:
                        account = Bank.CreateAccount<DebitAccount>(client, balance);
                        break;
                    case 2:
                        account = Bank.CreateAccount<CreditAccount>(client, balance);
                        break;
                    case 3:
                        account = Bank.CreateAccount<DepositAccount>(client, balance);
                        break;
                }

                Console.WriteLine("Guid of new account is {0}", account.Id);
            }
            catch
            {
                Console.WriteLine("You make a mistake with inputting data");
            }
        }

        public void ReadLaunch()
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(Bank.GetAccounts()));
        }

        public void RemoveLaunch()
        {
            try
            {
                Console.WriteLine("Write account guid");
                var accountId = new Guid(Console.ReadLine() ?? string.Empty);
                Console.WriteLine(Bank.RemoveAccount(Bank.FindAccount(accountId))
                    ? "Account was successfully deleted"
                    : "There was not such account");
            }
            catch
            {
                Console.WriteLine("You make a mistake with inputting data");
            }
        }
    }
}