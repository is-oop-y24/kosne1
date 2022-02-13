using System;
using System.Collections.Generic;
using IsuExtra.Entities.NamesOfUniversityStructures;
using IsuExtra.Entities.UniversityPeople;
using IsuExtra.Entities.UniversityStructure;

namespace IsuExtra.Services.UniversityStructureService
{
    public interface IUniversityManager
    {
        Faculty AddFaculty(char facultyName);
        Course AddCourse(Faculty faculty, CourseNumber courseNumber);
        Group AddGroup(GroupName groupName);
        Student AddStudent(Group group, string studentName);

        Student FindStudent(Guid id);
        Student FindStudent(string studentName);
        List<Student> FindStudents(GroupName groupName);

        Group FindGroup(GroupName groupName);

        void ChangeStudentGroup(Student student, Group newGroup);
    }
}