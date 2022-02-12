using System.Collections.Generic;
using IsuExtra.Entities.NamesOfUniversityStructures;

namespace IsuExtra.Entities.UniversityStructure
{
    public class Faculty
    {
        private List<Course> _courses;

        public Faculty(char facultyName)
        {
            FacultyName = facultyName;
            _courses = new List<Course>();
            for (int i = 1; i < 5; i++)
            {
                _courses.Add(new Course((CourseNumber)i));
            }
        }

        public char FacultyName { get; }
    }
}