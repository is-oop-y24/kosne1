using System;
using IsuExtra.Entities.NamesOfUniversityStructures;
using IsuExtra.Entities.ScheduleStructure;
using IsuExtra.Entities.UniversityFacilities;
using IsuExtra.Entities.UniversityPeople;

namespace IsuExtra.Services.ScheduleStructureService
{
    public interface IScheduleManager
    {
        GroupSchedule AddGroupSchedule(GroupName groupName);
        DaySchedule AddDaySchedule(DayOfWeek dayOfWeek, GroupName groupName);
        Lesson AddLesson(DayOfWeek dayOfWeek, LessonBeginning lessonBeginning, Teacher teacher, GroupName groupName, Auditorium auditorium);

        WeekSchedule FindGroupWeekSchedule(GroupName groupName);
        DaySchedule FindDaySchedule(DayOfWeek dayOfWeek, GroupName groupName);
        Lesson FindLesson(DayOfWeek dayOfWeek, LessonBeginning lessonBeginning, GroupName groupName);
    }
}