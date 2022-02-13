using System;
using IsuExtra.Entities.NamesOfUniversityStructures;

namespace IsuExtra.Entities.UniversityPeople
{
    public class Student
    {
        public Student(string name, GroupName groupName)
        {
            Id = Guid.NewGuid();
            Name = name;
            GroupName = groupName;
        }

        public Guid Id { get; }
        public string Name { get; }
        public GroupName GroupName { get; set; }
    }
}
