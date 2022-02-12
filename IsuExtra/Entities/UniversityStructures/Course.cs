using System.Collections.Generic;
using IsuExtra.Entities.NamesOfUniversityStructures;

namespace IsuExtra.Entities.UniversityStructures
{
    public class Course
    {
        private List<Group> _groups;

        public Course(CourseNumber courseNumber)
        {
            CourseNumber = courseNumber;
            _groups = new List<Group>();
        }

        public CourseNumber CourseNumber { get; }
    }
}