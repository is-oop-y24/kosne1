using System;
using System.Collections.Generic;

namespace IsuExtra.Entities.ScheduleStructure
{
    public class DaySchedule
    {
        private List<Lesson> _lessons;

        public DaySchedule(DayOfWeek dayOfWeek)
        {
            _lessons = new List<Lesson>();
            DayOfWeek = dayOfWeek;
        }

        public DayOfWeek DayOfWeek { get; }

        public void AddLesson(Lesson lesson)
        {
            _lessons.Add(lesson);
        }
    }
}