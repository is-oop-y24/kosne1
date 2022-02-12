using System;

namespace IsuExtra.Entities
{
    public class Student
    {
        public Student(string name, GroupName groupName)
        {
            Id = Guid.NewGuid();
            Name = name;
            Groupname = groupName;
        }

        public Guid Id { get; }
        public string Name { get; }
        public GroupName Groupname { get; }
    }
}
