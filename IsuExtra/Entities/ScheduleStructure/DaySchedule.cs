using System;
using System.Collections.Generic;
using System.Linq;
using IsuExtra.Entities.Interface;
using IsuExtra.Entities.UniversityFacilities;
using IsuExtra.Entities.UniversityPeople;
using IsuExtra.Services.DescriptionService;

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
        public IDescriptionStrategy DescriptionStrategy { get; set; }

        public List<Lesson> Lessons()
        {
            return new List<Lesson>(_lessons);
        }

        public Lesson AddLesson(LessonBeginning lessonBeginning, Teacher teacher, IGroupNames groupName, Auditorium auditorium)
        {
            var lesson = new Lesson(lessonBeginning, teacher, groupName, auditorium);
            _lessons.Add(lesson);
            return lesson;
        }

        public Lesson FindLesson(LessonBeginning lessonBeginning)
        {
            Lesson foundLesson =
                _lessons.FirstOrDefault(lesson => lesson.LessonBeginning == DescriptionStrategy.GetDescription(lessonBeginning));
            return foundLesson;
        }
    }
}