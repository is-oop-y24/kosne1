using System;
using System.Threading.Tasks;
using Client.NodesConsoleUI;

namespace Reports.Clients
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var consoleUi = new MainNode();
            await consoleUi.Launch();
        }
    }
}