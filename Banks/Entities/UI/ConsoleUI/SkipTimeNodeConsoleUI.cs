using System;
using Banks.Models;

namespace Banks.ConsoleUI
{
    public class SkipTimeNodeConsoleUI : NodeConsoleUI
    {
        public SkipTimeNodeConsoleUI(NodeConsoleUI parentNode)
            : base(parentNode, 2)
        {
        }

        public override void Launch()
        {
            while (true)
            {
                Console.WriteLine(" Menu \n 1.Skip days \n 2.Exit \n");

                int point = ReadMenuPoint();
                switch (point)
                {
                    case 1:
                        SkipLaunch();
                        break;
                    case 2:
                        Exit();
                        break;
                }
            }
        }

        private void SkipLaunch()
        {
            Console.WriteLine("How much days you want to skip?");
            int days = Convert.ToInt32(Console.ReadLine());
            SkipTimeService.SkipTime(days);
        }
    }
}