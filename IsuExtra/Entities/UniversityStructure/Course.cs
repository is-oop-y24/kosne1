using System;
using System.Collections.Generic;
using System.Linq;
using IsuExtra.Entities.NamesOfUniversityStructures;
using IsuExtra.Entities.UniversityPeople;

namespace IsuExtra.Entities.UniversityStructure
{
    public class Course
    {
        private List<Group> _groups;

        public Course(CourseNumber courseNumber)
        {
            CourseNumber = courseNumber;
            _groups = new List<Group>();
        }

        public CourseNumber CourseNumber { get; }

        public List<Group> Groups()
        {
            return new List<Group>(_groups);
        }

        public Group AddGroup(GroupName groupName)
        {
            var group = new Group(groupName);
            _groups.Add(group);

            return group;
        }

        public Group FindGroup(GroupName groupName)
        {
            Group foundGroup = _groups.FirstOrDefault(group => group.GroupName == groupName);
            return foundGroup;
        }

        public Student FindStudent(Guid id)
        {
            Student foundStudent = _groups.Select(group => group.FindStudent(id))
                .FirstOrDefault(student => student.Id == id);
            return foundStudent;
        }

        public Student FindStudent(string studentName)
        {
            Student foundStudent = _groups.Select(group => group.FindStudent(studentName))
                .FirstOrDefault(student => student.Name == studentName);
            return foundStudent;
        }
    }
}