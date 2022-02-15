using System.Collections.Generic;
using System.Linq;
using IsuExtra.Entities.NamesOfUniversityStructures;
using IsuExtra.Entities.UniversityPeople;

namespace IsuExtra.Entities.AEUniversityStructure
{
    public class AEUniversity
    {
        private List<AECourse> _courses;

        public AEUniversity()
        {
            _courses = new List<AECourse>();
        }

        public List<AECourse> Courses()
        {
            return new List<AECourse>(_courses);
        }

        public bool HaveCourse(string megaFaculty)
        {
            return _courses.Any(course => course.MegaFaculty == megaFaculty);
        }

        public AECourse FindCourse(string megaFaculty)
        {
            return _courses.Find(course => Equals(course.MegaFaculty, megaFaculty));
        }

        public AECourse AddCourse(string megaFaculty, char faculty)
        {
            var course = new AECourse(megaFaculty, faculty);
            _courses.Add(course);
            return course;
        }

        public Student FindStudent(string studentName)
        {
            return _courses.Select(course => course.FindStudent(studentName)).FirstOrDefault();
        }

        public List<Student> FindUnregisteredStudents(GroupName groupName)
        {
            return _courses.SelectMany(course => course.FindUnregisteredStudents(groupName)).ToList();
        }
    }
}