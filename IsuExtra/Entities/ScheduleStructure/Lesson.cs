using IsuExtra.Entities.Interface;
using IsuExtra.Entities.UniversityFacilities;
using IsuExtra.Entities.UniversityPeople;
using IsuExtra.Services.DescriptionService;

namespace IsuExtra.Entities.ScheduleStructure
{
    public class Lesson
    {
        public Lesson(LessonBeginning lessonBeginning, Teacher teacher, IGroupNames groupName, Auditorium auditorium)
        {
            DescriptionStrategy = new DescriptionStrategy();
            LessonBeginning = DescriptionStrategy.GetDescription(lessonBeginning);
            Teacher = teacher;
            GroupName = groupName;
            Auditorium = auditorium;
        }

        public string LessonBeginning { get; }
        public Teacher Teacher { get; }
        public IGroupNames GroupName { get; }
        public Auditorium Auditorium { get; }
        private IDescriptionStrategy DescriptionStrategy { get; set; }
    }
}