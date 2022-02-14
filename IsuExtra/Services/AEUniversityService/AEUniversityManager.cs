using System.Collections.Generic;
using System.Linq;
using IsuExtra.Entities.AEUniversityStructure;
using IsuExtra.Entities.NamesOfUniversityStructures;
using IsuExtra.Entities.UniversityPeople;
using IsuExtra.Services.UniversityStructureService;
using IsuExtra.Tools.SpecificExceptions;

namespace IsuExtra.Services.AEUniversityService
{
    public class AEUniversityManager : IAEUniversityManager
    {
        private AEUniversity _university;

        public AEUniversityManager()
        {
            _university = new AEUniversity();
        }

        public AECourse AddCourse(string megaFaculty)
        {
            if (FindCourse(megaFaculty) != default)
            {
                throw new AECourseException("Error: course already exist");
            }

            return _university.AddCourse(megaFaculty);
        }

        public AEGroup AddGroup(AEGroupName groupName)
        {
            if (FindGroup(groupName) != default)
            {
                throw new AEGroupException("Error: group already exist");
            }

            return FindCourse(groupName.MegaFaculty).AddGroup(groupName);
        }

        public Student AddStudent(AEGroup aeGroup, string studentName)
        {
            IUniversityManager universityManager = new UniversityManager();
            Student student = universityManager.FindStudent(studentName);

            if (student.AeGroups().Count == 2 || student.AeGroups().Any(name => name == aeGroup.GroupName)
                                              || aeGroup.Students().Count == AEGroup.MaximumNumberOfStudents)
            {
                throw new StudentException("Error: can not to add student to this group");
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

        public AECourse FindCourse(string megaFaculty)
        {
            return _university.FindCourse(megaFaculty);
        }

        public AEGroup FindGroup(AEGroupName groupName)
        {
            return FindCourse(groupName.MegaFaculty).FindGroup(groupName);
        }

        public List<AEGroup> FindGroups(string megaFaculty)
        {
            return _university.FindCourse(megaFaculty).Groups();
        }

        public List<Student> FindStudents(AEGroupName groupName)
        {
            List<Student> foundStudents = FindGroup(groupName).Students();
            if (foundStudents == default)
            {
                throw new AEGroupException("Error: group not found");
            }

            return foundStudents;
        }

        public List<Student> FindUnregisteredStudents(GroupName groupName)
        {
            IUniversityManager universityManager = new UniversityManager();
            return universityManager.FindStudents(groupName).FindAll(student => student.AeGroups().Count == 0);
        }
    }
}