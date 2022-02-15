﻿using System;
using System.Collections.Generic;
using System.Linq;
using IsuExtra.Entities.Interface;
using IsuExtra.Entities.ScheduleStructure;
using IsuExtra.Entities.UniversityFacilities;
using IsuExtra.Entities.UniversityPeople;
using IsuExtra.Tools.SpecificExceptions.ScheduleExceptions;

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
            var groupSchedule = new GroupSchedule(groupName);
            _groupSchedules.Add(groupSchedule);
            return groupSchedule;
        }

        public DaySchedule AddDaySchedule(DayOfWeek dayOfWeek, IGroupNames groupName)
        {
            if (HaveDaySchedule(dayOfWeek, groupName))
            {
                throw new DayScheduleException("Error: day schedule already exist");
            }

            return FindGroupWeekSchedule(groupName).AddDaySchedule(dayOfWeek);
        }

        public Lesson AddLesson(DayOfWeek dayOfWeek, LessonBeginning lessonBeginning, Teacher teacher, IGroupNames groupName, Auditorium auditorium)
        {
            if (HaveLesson(dayOfWeek, lessonBeginning, groupName))
            {
                throw new LessonException("Error: group already have lesson");
            }

            if (HaveLesson(dayOfWeek, lessonBeginning, teacher))
            {
                throw new LessonException("Error: teacher already have lesson");
            }

            if (HaveLesson(dayOfWeek, lessonBeginning, auditorium))
            {
                throw new LessonException("Error: auditorium already have lesson");
            }

            return FindDaySchedule(dayOfWeek, groupName)
                .AddLesson(dayOfWeek, lessonBeginning, teacher, groupName, auditorium);
        }

        public bool HaveGroupWeekSchedule(IGroupNames groupName)
        {
            return _groupSchedules.Any(groupSchedule => Equals(groupSchedule.GroupName, groupName));
        }

        public bool HaveDaySchedule(DayOfWeek dayOfWeek, IGroupNames groupName)
        {
            if (!HaveGroupWeekSchedule(groupName))
            {
                throw new WeekScheduleException("Error: week schedule does not exist");
            }

            return FindGroupWeekSchedule(groupName).HaveDaySchedule(dayOfWeek);
        }

        public bool HaveLesson(DayOfWeek dayOfWeek, LessonBeginning lessonBeginning, IGroupNames groupName)
        {
            if (!HaveDaySchedule(dayOfWeek, groupName))
            {
                throw new DayScheduleException("Error: day schedule does not exist");
            }

            return FindDaySchedule(dayOfWeek, groupName).HaveLesson(lessonBeginning);
        }

        public bool HaveLesson(DayOfWeek dayOfWeek, LessonBeginning lessonBeginning, Teacher teacher)
        {
            return FindLessons(dayOfWeek, lessonBeginning).Any(lesson => Equals(lesson.Teacher.Id, teacher.Id));
        }

        public bool HaveLesson(DayOfWeek dayOfWeek, LessonBeginning lessonBeginning, Auditorium auditorium)
        {
            return FindLessons(dayOfWeek, lessonBeginning).Any(lesson =>
                Equals(lesson.Auditorium.Address, auditorium.Address)
                && Equals(lesson.Auditorium.Number, auditorium.Number));
        }

        public bool ScheduleIntersect(IGroupNames groupName1, IGroupNames groupName2)
        {
            if (!HaveGroupWeekSchedule(groupName1))
            {
                throw new WeekScheduleException("Error: group1 week schedule does not exist");
            }

            if (!HaveGroupWeekSchedule(groupName2))
            {
                throw new WeekScheduleException("Error: group2 week schedule does not exist");
            }

            WeekSchedule weekSchedule1 = FindGroupWeekSchedule(groupName1);
            WeekSchedule weekSchedule2 = FindGroupWeekSchedule(groupName2);

            foreach (DaySchedule daySchedule1 in weekSchedule1.Days())
            {
                if (!weekSchedule2.HaveDaySchedule(daySchedule1.DayOfWeek)) continue;
                DaySchedule daySchedule2 = weekSchedule2.FindDaySchedule(daySchedule1.DayOfWeek);
                foreach (Lesson lesson1 in daySchedule1.Lessons())
                {
                    return daySchedule2.Lessons().Any(lesson2 => lesson2.LessonBeginning == lesson1.LessonBeginning);
                }
            }

            return false;
        }

        private List<Lesson> FindLessons(DayOfWeek dayOfWeek, LessonBeginning lessonBeginning)
        {
            return _groupSchedules.Select(groupSchedule => groupSchedule.WeekSchedule.FindDaySchedule(dayOfWeek).
                FindLesson(lessonBeginning)).Where(lesson => lesson != default).ToList();
        }

        private WeekSchedule FindGroupWeekSchedule(IGroupNames groupName)
        {
            return _groupSchedules.Find(groupSchedule => Equals(groupSchedule.GroupName, groupName))?.WeekSchedule;
        }

        private DaySchedule FindDaySchedule(DayOfWeek dayOfWeek, IGroupNames groupName)
        {
            return FindGroupWeekSchedule(groupName).FindDaySchedule(dayOfWeek);
        }
    }
}