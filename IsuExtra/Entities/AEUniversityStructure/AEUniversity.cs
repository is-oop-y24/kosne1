using System.Collections.Generic;
using System.Linq;

namespace IsuExtra.Entities.AEUniversityStructure
{
    public class AEUniversity
    {
        private List<AECourse> _courses;

        public AEUniversity()
        {
            _courses = new List<AECourse>();
        }

        public AECourse FindCourse(string megaFaculty)
        {
            AECourse course = _courses.FirstOrDefault(course => course.MegaFaculty == megaFaculty);
            return course;
        }

        public AECourse AddCourse(string megaFaculty, char faculty)
        {
            var course = new AECourse(megaFaculty, faculty);
            _courses.Add(course);
            return course;
        }
    }
}