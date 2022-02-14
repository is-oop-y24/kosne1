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

        public List<DaySchedule> Days()
        {
            return new List<DaySchedule>(_days);
        }

        public bool HaveDaySchedule(DayOfWeek dayOfWeek)
        {
            return _days.Any(daySchedule => Equals(daySchedule.DayOfWeek, dayOfWeek));
        }

        public DaySchedule FindDaySchedule(DayOfWeek dayOfWeek)
        {
            return _days.Find(daySchedule => Equals(daySchedule.DayOfWeek, dayOfWeek));
        }

        public DaySchedule AddDaySchedule(DayOfWeek dayOfWeek)
        {
            var daySchedule = new DaySchedule(dayOfWeek);
            _days.Add(daySchedule);
            return daySchedule;
        }
    }
}