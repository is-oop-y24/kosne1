using System;
using System.Collections.Generic;
using System.Linq;
using IsuExtra.Entities.NamesOfUniversityStructures;
using IsuExtra.Entities.UniversityPeople;

namespace IsuExtra.Entities.UniversityStructure
{
    public class Group
    {
        public const int MaximumNumberOfStudents = 25;
        private List<Student> _students;
        public Group(GroupName groupName)
        {
            Id = Guid.NewGuid();
            GroupName = groupName;
            _students = new List<Student>();
        }

        public Guid Id { get; }
        public GroupName GroupName { get; }

        public Student AddStudent(string studentName)
        {
            var student = new Student(studentName, GroupName);
            _students.Add(student);
            return student;
        }

        public void Remove(Student student)
        {
            _students.Remove(student);
        }

        public List<Student> Students()
        {
            return new List<Student>(_students);
        }

        public Student FindStudent(Guid id)
        {
            Student foundStudent = _students.FirstOrDefault(student => student.Id == id);
            return foundStudent;
        }

        public Student FindStudent(string studentName)
        {
            Student foundStudent = _students.FirstOrDefault(student => student.Name == studentName);
            return foundStudent;
        }
    }
}