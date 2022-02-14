using System.Collections.Generic;

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
    }
}