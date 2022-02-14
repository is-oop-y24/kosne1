using System;
using System.Collections.Generic;
using System.Linq;
using IsuExtra.Entities.NamesOfUniversityStructures;
using IsuExtra.Entities.UniversityPeople;

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

        public Course AddCourse(CourseNumber courseNumber)
        {
            var course = new Course(courseNumber);
            _courses.Add(course);

            return course;
        }

        public Course FindCourse(CourseNumber courseNumber)
        {
            Course foundCourse = _courses.FirstOrDefault(course => course.CourseNumber == courseNumber);
            return foundCourse;
        }

        public Group FindGroup(GroupName groupName)
        {
            Group foundGroup = _courses.Select(course => course.FindGroup(groupName)).
                FirstOrDefault(foundGroup => foundGroup.GroupName == groupName);
            return foundGroup;
        }

        public Student FindStudent(Guid id)
        {
            Student foundStudent = _courses.Select(course => course.FindStudent(id))
                .FirstOrDefault(student => student.Id == id);
            return foundStudent;
        }

        public Student FindStudent(string studentName)
        {
            Student foundStudent = _courses.Select(faculty => faculty.FindStudent(studentName))
                .FirstOrDefault(student => student.Name == studentName);
            return foundStudent;
        }
    }
}