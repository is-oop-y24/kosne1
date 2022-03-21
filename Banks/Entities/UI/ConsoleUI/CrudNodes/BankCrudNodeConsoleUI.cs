using System;
using Banks.Entities.Banks;
using Banks.Entities.Banks.Conditions;

namespace Banks.ConsoleUI.CrudNodes
{
    public class BankCrudNodeConsoleUI : NodeConsoleUI
    {
        private InBankCrudConsoleUI _inBankCrudConsoleUi;
        public BankCrudNodeConsoleUI(NodeConsoleUI parentNode)
            : base(parentNode, 4)
        {
            _inBankCrudConsoleUi = new InBankCrudConsoleUI(this);
        }

        public override void Launch()
        {
            while (true)
            {
                Console.WriteLine(" Menu \n 1.Create bank \n 2.Read banks \n 3.Go into bank \n 4.Exit \n");
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
                        GoIntoLaunch();
                        break;
                    case 4:
                        Exit();
                        break;
                }
            }
        }

        private void CreateLaunch()
        {
            try
            {
                Console.WriteLine("Write bank name");
                string bankName = Console.ReadLine();

                Console.WriteLine("Write credit limit and commission");
                decimal creditLimit = Convert.ToDecimal(Console.ReadLine());
                decimal creditCommission = Convert.ToDecimal(Console.ReadLine());
                var creditCondition = new CreditCondition(creditLimit, creditCommission);

                Console.WriteLine("Write debit day multiplier");
                double debitMultiplier = Convert.ToDouble(Console.ReadLine());
                var debitCondition = new DebitCondition(debitMultiplier);

                Console.WriteLine("Write max interest");
                double maxInterest = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Write amount for max interest");
                decimal amount = Convert.ToDecimal(Console.ReadLine());
                int depositConditionsSize = Convert.ToInt32(Console.ReadLine());

                var depositCondition = new DepositCondition(maxInterest, amount, new TimeSpan(365, 0, 0, 0));

                Console.WriteLine("Write max transaction for not confirmed");
                decimal maxTransaction = Convert.ToDecimal(Console.ReadLine());
                var notConfirmedCondition = new UnconfirmedCondition(maxTransaction);

                var bankInfo = new BankConditions(notConfirmedCondition, debitCondition, depositCondition, creditCondition);
                var newBank = new Bank(bankName, bankInfo);
                CentralBank.Instance.AddBank(newBank);
                Console.WriteLine("Your bank id: " + newBank.Id);
            }
            catch
            {
                Console.WriteLine("You make a mistake with inputting data");
            }
        }

        private void ReadLaunch()
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(CentralBank.Instance.GetBanks()));
        }

        private void GoIntoLaunch()
        {
            try
            {
                Console.WriteLine("Write bank guid");
                var bankGuid = new Guid(Console.ReadLine() ?? string.Empty);
                Bank bank = CentralBank.Instance.FindBank(bankGuid);
                _inBankCrudConsoleUi.Bank = bank;
                _inBankCrudConsoleUi.Launch();
            }
            catch
            {
                Console.WriteLine("You make a mistake with inputting data");
            }
        }
    }
}