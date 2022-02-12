using System;
using System.Collections.Generic;
using IsuExtra.Entities.NamesOfUniversityStructures;
using IsuExtra.Entities.UniversityPeople;

namespace IsuExtra.Entities.UniversityStructure
{
    public class Group
    {
        private List<Student> _students;
        public Group(GroupName groupName)
        {
            Id = Guid.NewGuid();
            GroupName = groupName;
            _students = new List<Student>();
        }

        public Guid Id { get; }
        public GroupName GroupName { get; }

        public List<Student> StudentList()
        {
            return new List<Student>(_students);
        }

        public void AddStudent(Student student)
        {
            _students.Add(student);
        }

        public void Remove(Student student)
        {
            _students.Remove(student);
        }
    }
}