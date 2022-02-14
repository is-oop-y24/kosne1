using System.Collections.Generic;
using IsuExtra.Entities.NamesOfUniversityStructures;
using IsuExtra.Entities.UniversityPeople;

namespace IsuExtra.Entities.AEUniversityStructure
{
    public class AEGroup
    {
        private List<Student> _students;

        public AEGroup(AEGroupName groupName)
        {
            GroupName = groupName;
            _students = new List<Student>();
        }

        public AEGroupName GroupName { get; }
    }
}