using System;
using Banks.ConsoleUI;
using Banks.Entities;
using Banks.Entities.Banks;
using Newtonsoft.Json;

namespace Banks.UI.ConsoleUI.CrudNodes
{
    public class ClientCrudNodeConsoleUI : NodeConsoleUI
    {
        public ClientCrudNodeConsoleUI(NodeConsoleUI parentNode)
        : base(parentNode, 4)
        {
        }

        public Bank Bank { get; set; }
        public override void Launch()
        {
             while (true)
             {
                 Console.WriteLine(" Menu \n 1.Create Client \n 2.Read clients \n 3.Update Client \n 4.Exit \n");
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
                         UpdateLaunch();
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
                Console.WriteLine("Write first name of new client");
                string firstName = Console.ReadLine();
                Console.WriteLine("Write second name of new client");
                string secondName = Console.ReadLine();
                Client newClient = Bank.AddClient(firstName, secondName);
                Console.WriteLine("Client guid: {0}", newClient.Id);
            }
            catch
            {
                Console.WriteLine("You make a mistake with inputting data");
            }
        }

        private void UpdateLaunch()
        {
            try
            {
                Console.WriteLine("Write guid of updating client");
                var clientId = new Guid(Console.ReadLine() ?? string.Empty);
                Client client = Bank.FindClient(clientId);
                Console.WriteLine("Choose what info you want to update\n 1.Address \n 2.Passport");
                int point = ReadMenuPoint(2);
                switch (point)
                {
                    case 1:
                        Console.WriteLine("Write new address info");
                        client.AddAddress(Console.ReadLine());
                        break;
                    case 2:
                        Console.WriteLine("Write new passport info");
                        client.AddPassportNumber(Console.ReadLine());
                        break;
                }
            }
            catch
            {
                Console.WriteLine("You make a mistake with inputting data");
            }
        }

        private void ReadLaunch()
        {
            Console.WriteLine(JsonConvert.SerializeObject(Bank.GetClients()));
        }
    }
}