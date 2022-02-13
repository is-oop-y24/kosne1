using System;
using System.Collections.Generic;
using IsuExtra.Entities.NamesOfUniversityStructures;
using IsuExtra.Entities.UniversityPeople;
using IsuExtra.Entities.UniversityStructure;
using IsuExtra.Tools.SpecificExceptions;

namespace IsuExtra.Services.UniversityStructureService
{
    public class UniversityManager : IUniversityManager
    {
        private University _university;

        public UniversityManager()
        {
            _university = new University();
        }

        public Faculty AddFaculty(char facultyName)
        {
            Faculty faculty = _university.AddFaculty(facultyName);
            return faculty;
        }

        public Course AddCourse(Faculty faculty, CourseNumber courseNumber)
        {
            Course course = faculty.AddCourse(courseNumber);
            return course;
        }

        public Group AddGroup(GroupName groupName)
        {
            if (FindGroupBool(groupName))
            {
                throw new GroupException("Error: group already exist");
            }

            if (_university.FindFaculty(groupName.Faculty) == default)
            {
                throw new FacultyException("Error: faculty does not exist");
            }

            Course course = _university.FindCourse(groupName.CourseNumber);
            if (course == default)
            {
                throw new CourseException("Error: course does not exist");
            }

            Group group = course.AddGroup(groupName);
            return group;
        }

        public Student AddStudent(Group group, string studentName)
        {
            if (group.Students().Count == Group.MaximumNumberOfStudents)
            {
                throw new GroupException("Error: maximum number of students has been reached");
            }

            Student student = group.AddStudent(studentName);
            return student;
        }

        public Student FindStudent(Guid id)
        {
            Student foundStudent = _university.FindStudent(id);

            if (foundStudent != default)
            {
                return foundStudent;
            }

            throw new StudentException("Error: student not found");
        }

        public Student FindStudent(string studentName)
        {
            Student foundStudent = _university.FindStudent(studentName);

            if (foundStudent != default)
            {
                return foundStudent;
            }

            throw new StudentException("Error: student not found");
        }

        public List<Student> FindStudents(GroupName groupName)
        {
            return FindGroup(groupName).Students();
        }

        public Group FindGroup(GroupName groupName)
        {
            Group foundGroup = _university.FindGroup(groupName);

            if (foundGroup != default)
            {
                return foundGroup;
            }

            throw new GroupException("Error: group not found");
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            throw new NotImplementedException();
        }

        private bool FindGroupBool(GroupName groupName)
        {
            Group foundGroup = _university.FindGroup(groupName);

            return foundGroup != default;
        }
    }
}