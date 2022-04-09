using System;
using System.Collections.Generic;
using IsuExtra.Entities.NamesOfUniversityStructures;

namespace IsuExtra.Entities.UniversityPeople
{
    public class Student
    {
        private List<AEGroupName> _aeGroups;
        public Student(string name, GroupName groupName)
        {
            Id = Guid.NewGuid();
            Name = name;
            GroupName = groupName;
            _aeGroups = new List<AEGroupName>();
        }

        public Guid Id { get; }
        public string Name { get; }
        public GroupName GroupName { get; set; }

        public List<AEGroupName> AeGroups()
        {
            return new List<AEGroupName>(_aeGroups);
        }

        public void AddAEGroup(AEGroupName groupName)
        {
            _aeGroups.Add(groupName);
        }

        public void RemoveAEGroup(AEGroupName groupName)
        {
            _aeGroups.Remove(groupName);
        }
    }
}
