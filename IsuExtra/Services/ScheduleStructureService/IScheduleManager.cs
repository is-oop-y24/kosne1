using System;
using IsuExtra.Entities.Interface;
using IsuExtra.Entities.ScheduleStructure;
using IsuExtra.Entities.UniversityFacilities;
using IsuExtra.Entities.UniversityPeople;

namespace IsuExtra.Services.ScheduleStructureService
{
    public interface IScheduleManager
    {
        GroupSchedule AddGroupSchedule(IGroupNames groupName);
        DaySchedule AddDaySchedule(DayOfWeek dayOfWeek, IGroupNames groupName);
        Lesson AddLesson(DayOfWeek dayOfWeek, LessonBeginning lessonBeginning, Teacher teacher, IGroupNames groupName, Auditorium auditorium);

        bool HaveGroupWeekSchedule(IGroupNames groupName);
        bool HaveDaySchedule(DayOfWeek dayOfWeek, IGroupNames groupName);
        bool HaveLesson(DayOfWeek dayOfWeek, LessonBeginning lessonBeginning, IGroupNames groupName);

        bool HaveLesson(DayOfWeek dayOfWeek, LessonBeginning lessonBeginning, Teacher teacher);
        bool HaveLesson(DayOfWeek dayOfWeek, LessonBeginning lessonBeginning, Auditorium auditorium);

        bool ScheduleIntersect(IGroupNames groupName1, IGroupNames groupName2);
    }
}