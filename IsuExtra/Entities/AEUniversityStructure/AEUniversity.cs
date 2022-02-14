using System.Collections.Generic;

namespace IsuExtra.Entities.AEUniversityStructure
{
    public class AEUniversity
    {
        private List<AECourse> _courses;

        public AEUniversity()
        {
            _courses = new List<AECourse>();
        }
    }
}