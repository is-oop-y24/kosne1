using System;
using System.Collections.Generic;
using System.Linq;

namespace IsuExtra.Entities.ScheduleStructure
{
    public class WeekSchedule
    {
        private List<DaySchedule> _days;

        public WeekSchedule()
        {
            _days = new List<DaySchedule>();
        }

        public DaySchedule AddDaySchedule(DayOfWeek dayOfWeek)
        {
            var daySchedule = new DaySchedule(dayOfWeek);
            _days.Add(daySchedule);
            return daySchedule;
        }

        public DaySchedule FindDaySchedule(DayOfWeek dayOfWeek)
        {
            DaySchedule daySchedule = _days.FirstOrDefault(daySchedule => daySchedule.DayOfWeek == dayOfWeek);
            return daySchedule;
        }
    }
}