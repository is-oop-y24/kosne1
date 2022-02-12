using IsuExtra.Entities.NamesOfUniversityStructures;
using IsuExtra.Entities.UniversityFacilities;
using IsuExtra.Entities.UniversityPeople;
using IsuExtra.Services.DescriptionService;

namespace IsuExtra.Entities.ScheduleStructure
{
    public class Lesson
    {
        public Lesson(LessonBeginning lessonBeginning, Teacher teacher, GroupName groupName, Auditorium auditorium)
        {
            DescriptionStrategy = new DescriptionStrategy();
            LessonBeginning = DescriptionStrategy.GetDescription(lessonBeginning);
            Teacher = teacher;
            GroupName = groupName;
            Auditorium = auditorium;
        }

        public string LessonBeginning { get; }
        public Teacher Teacher { get; }
        public GroupName GroupName { get; }
        public Auditorium Auditorium { get; }
        public IDescriptionStrategy DescriptionStrategy { private get; set; }
    }
}