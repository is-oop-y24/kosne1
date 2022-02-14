﻿using System;
using System.Collections.Generic;
using System.Linq;
using IsuExtra.Entities.NamesOfUniversityStructures;
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

        public GroupSchedule AddGroupSchedule(GroupName groupName)
        {
            if (FindGroupSchedule(groupName) != default)
            {
                throw new GroupScheduleException("Error: group schedule already exist");
            }

            var groupSchedule = new GroupSchedule(groupName);
            _groupSchedules.Add(groupSchedule);
            return groupSchedule;
        }

        public DaySchedule AddDaySchedule(DayOfWeek dayOfWeek, GroupName groupName)
        {
            if (FindDaySchedule(dayOfWeek, groupName) != default)
            {
                throw new DayScheduleException("Error: day schedule already exist");
            }

            DaySchedule daySchedule = FindGroupWeekSchedule(groupName).AddDaySchedule(dayOfWeek);
            return daySchedule;
        }

        public Lesson AddLesson(DayOfWeek dayOfWeek, LessonBeginning lessonBeginning, Teacher teacher, GroupName groupName, Auditorium auditorium)
        {
            if (FindLesson(dayOfWeek, lessonBeginning, groupName) != default)
            {
                throw new LessonException("Error: lesson already exist");
            }

            Lesson lesson = FindDaySchedule(dayOfWeek, groupName)
                .AddLesson(lessonBeginning, teacher, groupName, auditorium);
            return lesson;
        }

        public WeekSchedule FindGroupWeekSchedule(GroupName groupName)
        {
            GroupSchedule groupSchedule = FindGroupSchedule(groupName);
            if (groupSchedule == default)
            {
                throw new GroupScheduleException("Error: group schedule does not exist");
            }

            return groupSchedule.WeekSchedule;
        }

        public DaySchedule FindDaySchedule(DayOfWeek dayOfWeek, GroupName groupName)
        {
            WeekSchedule weekSchedule = FindGroupWeekSchedule(groupName);
            if (weekSchedule == default)
            {
                throw new WeekScheduleException("Error: week schedule does not exist");
            }

            DaySchedule foundDaySchedule = weekSchedule.FindDaySchedule(dayOfWeek);
            return foundDaySchedule;
        }

        public Lesson FindLesson(DayOfWeek dayOfWeek, LessonBeginning lessonBeginning, GroupName groupName)
        {
            DaySchedule daySchedule = FindDaySchedule(dayOfWeek, groupName);
            if (daySchedule == default)
            {
                throw new DayScheduleException("Error: day schedule does not exist");
            }

            Lesson foundLesson = daySchedule.FindLesson(lessonBeginning);
            return foundLesson;
        }

        private GroupSchedule FindGroupSchedule(GroupName groupName)
        {
            GroupSchedule foundGroupSchedule =
                _groupSchedules.FirstOrDefault(groupSchedule => groupSchedule.GroupName == groupName);
            return foundGroupSchedule;
        }
    }
}