using System.Collections.Generic;

namespace IsuExtra.Entities
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