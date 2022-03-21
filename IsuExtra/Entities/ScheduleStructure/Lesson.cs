using System;
using IsuExtra.Entities.GroupInterface;
using IsuExtra.Entities.UniversityFacilities;
using IsuExtra.Entities.UniversityPeople;
using IsuExtra.Services.DescriptionService;

namespace IsuExtra.Entities.ScheduleStructure
{
    public class Lesson : DescriptionStrategy
    {
        public Lesson(DayOfWeek dayOfWeek, LessonBeginning lessonBeginning, Teacher teacher, IGroupNames groupName, Auditorium auditorium)
        {
            DayOfWeek = dayOfWeek;
            LessonBeginning = GetDescription(lessonBeginning);
            Teacher = teacher;
            GroupName = groupName;
            Auditorium = auditorium;
        }

        public DayOfWeek DayOfWeek { get; }
        public string LessonBeginning { get; }
        public Teacher Teacher { get; }
        public IGroupNames GroupName { get; }
        public Auditorium Auditorium { get; }
    }
}