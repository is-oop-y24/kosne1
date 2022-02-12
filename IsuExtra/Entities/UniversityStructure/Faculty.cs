using System.Collections.Generic;

namespace IsuExtra.Entities.UniversityStructure
{
    public class Faculty
    {
        private List<Course> _courses;

        public Faculty(char facultyName)
        {
            FacultyName = facultyName;
            _courses = new List<Course>();
        }

        public char FacultyName { get; }

        public List<Course> Courses()
        {
            return new List<Course>(_courses);
        }
    }
}