using System;
using Banks.Entities.Banks;

namespace Banks.Entities
{
    public class TimeMachine
    {
        public static void RewindTime(int days)
        {
            DateTime current = DateTime.Today;
            for (int i = 0; i < days; i++)
            {
                if (current.Day == 1) CentralBank.Instance.MakeMonthlyAddition(current);
                CentralBank.Instance.MakeDailyAddition(current);
            }
        }
    }
}