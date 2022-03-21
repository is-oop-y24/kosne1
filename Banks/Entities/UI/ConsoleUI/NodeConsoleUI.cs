using System;

namespace Banks.ConsoleUI
{
    public abstract class NodeConsoleUI
    {
        private int _menuPointCount;
        protected NodeConsoleUI(NodeConsoleUI parentNode, int menuPointCount)
        {
            ParentNode = parentNode;
            _menuPointCount = menuPointCount;
        }

        protected NodeConsoleUI ParentNode { get; }
        public abstract void Launch();

        public void Exit()
        {
            ParentNode.Launch();
        }

        protected int ReadMenuPoint(int maxPoint = -1)
        {
            if (maxPoint < 0) maxPoint = _menuPointCount;

            int point = -1;
            while (true)
            {
                try
                {
                    string value = Console.ReadLine();
                    point = Convert.ToInt32(value.Trim());
                    if (!(point >= 1 && point <= maxPoint))
                        throw new FormatException("Wrong format of menu point");
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please write number from 1 to " + _menuPointCount);
                }
            }

            return point;
        }
    }
}