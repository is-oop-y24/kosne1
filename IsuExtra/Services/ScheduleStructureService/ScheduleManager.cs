using System;
using System.Collections.Generic;
using System.Linq;
using IsuExtra.Entities.Interface;
using IsuExtra.Entities.ScheduleStructure;
using IsuExtra.Entities.UniversityFacilities;
using IsuExtra.Entities.UniversityPeople;
using IsuExtra.Tools.SpecificExceptions;

namespace IsuExtra.Services.ScheduleStructureService
{
    public class ScheduleManager : IScheduleManager
    {
        private List<GroupSchedule> _groupSchedules;

        public ScheduleManager()
        {
            _groupSchedules = new List<GroupSchedule>();
        }

        public GroupSchedule AddGroupSchedule(IGroupNames groupName)
        {
            if (FindGroupSchedule(groupName) != default)
            {
                throw new GroupScheduleException("Error: group schedule already exist");
            }

            var groupSchedule = new GroupSchedule(groupName);
            _groupSchedules.Add(groupSchedule);
            return groupSchedule;
        }

        public DaySchedule AddDaySchedule(DayOfWeek dayOfWeek, IGroupNames groupName)
        {
            if (FindDaySchedule(dayOfWeek, groupName) != default)
            {
                throw new DayScheduleException("Error: day schedule already exist");
            }

            DaySchedule daySchedule = FindGroupWeekSchedule(groupName).AddDaySchedule(dayOfWeek);
            return daySchedule;
        }

        public Lesson AddLesson(DayOfWeek dayOfWeek, LessonBeginning lessonBeginning, Teacher teacher, IGroupNames groupName, Auditorium auditorium)
        {
            if (FindLesson(dayOfWeek, lessonBeginning, groupName) != default)
            {
                throw new LessonException("Error: lesson already exist");
            }

            if (HaveLesson(dayOfWeek, lessonBeginning, teacher))
            {
                throw new LessonException("Error: teacher already have lesson");
            }

            if (HaveLesson(dayOfWeek, lessonBeginning, auditorium))
            {
                throw new LessonException("Error: auditorium already have lesson");
            }

            Lesson lesson = FindDaySchedule(dayOfWeek, groupName)
                .AddLesson(lessonBeginning, teacher, groupName, auditorium);
            return lesson;
        }

        public WeekSchedule FindGroupWeekSchedule(IGroupNames groupName)
        {
            GroupSchedule groupSchedule = FindGroupSchedule(groupName);
            if (groupSchedule == default)
            {
                throw new GroupScheduleException("Error: group schedule does not exist");
            }

            return groupSchedule.WeekSchedule;
        }

        public DaySchedule FindDaySchedule(DayOfWeek dayOfWeek, IGroupNames groupName)
        {
            WeekSchedule weekSchedule = FindGroupWeekSchedule(groupName);
            if (weekSchedule == default)
            {
                throw new WeekScheduleException("Error: week schedule does not exist");
            }

            DaySchedule foundDaySchedule = weekSchedule.FindDaySchedule(dayOfWeek);
            return foundDaySchedule;
        }

        public Lesson FindLesson(DayOfWeek dayOfWeek, LessonBeginning lessonBeginning, IGroupNames groupName)
        {
            DaySchedule daySchedule = FindDaySchedule(dayOfWeek, groupName);
            if (daySchedule == default)
            {
                throw new DayScheduleException("Error: day schedule does not exist");
            }

            Lesson foundLesson = daySchedule.FindLesson(lessonBeginning);
            return foundLesson;
        }

        public bool HaveLesson(DayOfWeek dayOfWeek, LessonBeginning lessonBeginning, Teacher teacher)
        {
            List<Lesson> foundLessons = FindLessons(dayOfWeek, lessonBeginning);
            return foundLessons.Any(lesson => lesson.Teacher.Id == teacher.Id);
        }

        public bool HaveLesson(DayOfWeek dayOfWeek, LessonBeginning lessonBeginning, Auditorium auditorium)
        {
            List<Lesson> foundLessons = FindLessons(dayOfWeek, lessonBeginning);
            return foundLessons.Any(lesson =>
                lesson.Auditorium.Number == auditorium.Number && lesson.Auditorium.Address == auditorium.Address);
        }

        private List<Lesson> FindLessons(DayOfWeek dayOfWeek, LessonBeginning lessonBeginning)
        {
            var lessons = _groupSchedules.Select(groupSchedule => FindLesson(dayOfWeek, lessonBeginning, groupSchedule.GroupName)).ToList();
            return lessons;
        }

        private GroupSchedule FindGroupSchedule(IGroupNames groupName)
        {
            GroupSchedule foundGroupSchedule =
                _groupSchedules.FirstOrDefault(groupSchedule => groupSchedule.GroupName == groupName);
            return foundGroupSchedule;
        }
    }
}