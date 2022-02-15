using System;
using System.Collections.Generic;
using System.Linq;
using IsuExtra.Entities.AEUniversityStructure;
using IsuExtra.Entities.NamesOfUniversityStructures;
using IsuExtra.Entities.UniversityPeople;
using IsuExtra.Tools.SpecificExceptions.AEExceptions;
using IsuExtra.Tools.SpecificExceptions.UniversityPeopleException;
using IsuExtra.Tools.SpecificExceptions.UniversityStructureExceptions;

namespace IsuExtra.Services.AEUniversityService
{
    public class AEUniversityManager : IAEUniversityManager
    {
        private AEUniversity _university;

        public AEUniversityManager()
        {
            _university = new AEUniversity();
        }

        public AECourse AddCourse(string megaFaculty, char faculty)
        {
            return _university.AddCourse(megaFaculty, faculty);
        }

        public AEGroup AddGroup(AEGroupName groupName)
        {
            if (HaveGroup(groupName))
            {
                throw new AEGroupException("Error: group already exist");
            }

            return FindCourse(groupName.MegaFaculty).AddGroup(groupName);
        }

        public Student AddStudent(AEGroup aeGroup, Student student)
        {
            if (aeGroup.Students().Count == AEGroup.MaximumNumberOfStudents)
            {
                throw new AEGroupException("Error: the maximum number of students has been reached in the AE group");
            }

            if (FindCourse(aeGroup.GroupName.MegaFaculty).Faculty == student.GroupName.Faculty)
            {
                throw new StudentException("Error: can not add student in this group");
            }

            student.AddAEGroup(aeGroup.GroupName);
            aeGroup.AddStudent(student);
            return student;
        }

        public Student RemoveStudent(AEGroup aeGroup, string studentName)
        {
            IUniversityManager universityManager = new UniversityManager();
            Student student = universityManager.FindStudent(studentName);

            if (student.AeGroups().All(name => name != aeGroup.GroupName))
            {
                throw new StudentException("Error: can not to remove student to this group");
            }

            student.RemoveAEGroup(aeGroup.GroupName);
            aeGroup.RemoveStudent(student);
            return student;
        }

        public bool HaveCourse(string megaFaculty)
        {
            return _university.HaveCourse(megaFaculty);
        }

        public bool HaveGroup(AEGroupName groupName)
        {
            if (!HaveCourse(groupName.MegaFaculty))
            {
                throw new AECourseException("Error: AE course does not exist");
            }

            return FindCourse(groupName.MegaFaculty).HaveGroup(groupName);
        }

        public List<AEGroup> FindGroups(string megaFaculty)
        {
            return _university.Courses().Where(course => course.MegaFaculty == megaFaculty)
                .SelectMany(course => course.Groups()).ToList();
        }

        public List<Student> FindStudents(AEGroupName groupName)
        {
            if (!HaveGroup(groupName))
            {
                throw new AEGroupException("Error: group not found");
            }

            return FindGroup(groupName).Students();
        }

        public List<Student> FindUnregisteredStudents(GroupName groupName)
        {
            List<Student> students = _university.FindUnregisteredStudents(groupName);

            if (students == default)
            {
                throw new ListStudentException("Error: students not found");
            }

            return students;
        }

        private AECourse FindCourse(string megaFaculty)
        {
            return _university.FindCourse(megaFaculty);
        }

        private AEGroup FindGroup(AEGroupName groupName)
        {
            return FindCourse(groupName.MegaFaculty).FindGroup(groupName);
        }

        private Student FindStudent(string studentName)
        {
            Student foundStudent = _university.FindStudent(studentName);
            if (foundStudent == default)
            {
                throw new AEGroupException("Error: student not found");
            }

            return foundStudent.Name != studentName ? null : foundStudent;
        }
    }
}