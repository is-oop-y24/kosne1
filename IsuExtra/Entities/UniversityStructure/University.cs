using System;
using System.Collections.Generic;
using System.Linq;
using IsuExtra.Entities.NamesOfUniversityStructures;
using IsuExtra.Entities.UniversityPeople;

namespace IsuExtra.Entities.UniversityStructure
{
    public class University
    {
        private List<Faculty> _faculties;

        public University()
        {
            _faculties = new List<Faculty>();
        }

        public List<Faculty> Faculties()
        {
            return new List<Faculty>(_faculties);
        }

        public Faculty AddFaculty(char facultyName)
        {
            var faculty = new Faculty(facultyName);

            return faculty;
        }

        public Faculty FindFaculty(char facultyName)
        {
            Faculty foundFaculty = _faculties.FirstOrDefault(faculty => faculty.FacultyName == facultyName);
            return foundFaculty;
        }

        public Course FindCourse(CourseNumber courseNumber)
        {
            Course foundCourse = _faculties.Select(faculty => faculty.FindCourse(courseNumber))
                .FirstOrDefault(course => course.CourseNumber == courseNumber);
            return foundCourse;
        }

        public Group FindGroup(GroupName groupName)
        {
            Group foundGroup = _faculties.Select(faculty => faculty.FindGroup(groupName)).
                FirstOrDefault(group => group.GroupName == groupName);
            return foundGroup;
        }

        public Student FindStudent(Guid id)
        {
            Student foundStudent = _faculties.Select(faculty => faculty.FindStudent(id))
                .FirstOrDefault(student => student.Id == id);
            return foundStudent;
        }

        public Student FindStudent(string studentName)
        {
            Student foundStudent = _faculties.Select(faculty => faculty.FindStudent(studentName))
                .FirstOrDefault(student => student.Name == studentName);
            return foundStudent;
        }
    }
}