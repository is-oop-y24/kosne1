using System.Collections.Generic;
using IsuExtra.Entities.NamesOfUniversityStructures;
using IsuExtra.Entities.UniversityPeople;

namespace IsuExtra.Entities.AEUniversityStructure
{
    public class AEGroup
    {
        public const int MaximumNumberOfStudents = 25;
        private List<Student> _students;

        public AEGroup(AEGroupName groupName)
        {
            GroupName = groupName;
            _students = new List<Student>();
        }

        public AEGroupName GroupName { get; }

        public List<Student> Students()
        {
            return new List<Student>(_students);
        }
    }
}