using System;
using System.Collections.Generic;

namespace IsuExtra.Entities.ScheduleStructure
{
    public class WeekSchedule
    {
        private List<DaySchedule> _days;

        public WeekSchedule()
        {
            _days = new List<DaySchedule>();
            for (int i = 0; i < 7; i++)
            {
                _days.Add(new DaySchedule((DayOfWeek)i));
            }
        }
    }
}