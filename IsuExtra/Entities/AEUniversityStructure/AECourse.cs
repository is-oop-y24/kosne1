using System.Collections.Generic;
using System.Linq;
using IsuExtra.Entities.NamesOfUniversityStructures;

namespace IsuExtra.Entities.AEUniversityStructure
{
    public class AECourse
    {
        private List<AEGroup> _groups;

        public AECourse(string megaFaculty)
        {
            MegaFaculty = megaFaculty;
            _groups = new List<AEGroup>();
        }

        public string MegaFaculty { get; }

        public List<AEGroup> Groups()
        {
            return new List<AEGroup>(_groups);
        }

        public AEGroup FindGroup(AEGroupName groupName)
        {
            return _groups.FirstOrDefault(group => group.GroupName == groupName);
        }

        public AEGroup AddGroup(AEGroupName groupName)
        {
            var group = new AEGroup(groupName);
            _groups.Add(group);
            return group;
        }
    }
}