using System.Collections.Generic;
using System.Linq;
using IsuExtra.Entities.NamesOfUniversityStructures;
using IsuExtra.Entities.UniversityPeople;

namespace IsuExtra.Entities.AEUniversityStructure
{
    public class AECourse
    {
        private List<AEGroup> _groups;

        public AECourse(string megaFaculty, char faculty)
        {
            Faculty = faculty;
            MegaFaculty = megaFaculty;
            _groups = new List<AEGroup>();
        }

        public string MegaFaculty { get; }
        public char Faculty { get; }

        public List<AEGroup> Groups()
        {
            return new List<AEGroup>(_groups);
        }

        public bool HaveGroup(AEGroupName groupName)
        {
            return _groups.Any(group => group.GroupName == groupName);
        }

        public AEGroup FindGroup(AEGroupName groupName)
        {
            return _groups.Find(group => group.GroupName == groupName);
        }

        public AEGroup AddGroup(AEGroupName groupName)
        {
            var group = new AEGroup(groupName);
            _groups.Add(group);
            return group;
        }

        public Student FindStudent(string studentName)
        {
            return _groups.Select(group => group.FindStudent(studentName)).FirstOrDefault();
        }

        public List<Student> FindUnregisteredStudents(GroupName groupName)
        {
            return _groups.SelectMany(group => group.FindUnregisteredStudents(groupName)).ToList();
        }
    }
}