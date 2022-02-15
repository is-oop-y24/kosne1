using System.Collections.Generic;
using IsuExtra.Entities.AEUniversityStructure;
using IsuExtra.Entities.NamesOfUniversityStructures;
using IsuExtra.Entities.UniversityPeople;

namespace IsuExtra.Services.AEUniversityService
{
    public interface IAEUniversityManager
    {
        AECourse AddCourse(string megaFaculty, char faculty);
        AEGroup AddGroup(AEGroupName groupName);

        Student AddStudent(AEGroup aeGroup, string studentName);
        Student RemoveStudent(AEGroup aeGroup, string studentName);

        bool HaveCourse(string megaFaculty);
        bool HaveGroup(AEGroupName groupName);

        List<AEGroup> FindGroups(string megaFaculty);
        List<Student> FindStudents(AEGroupName groupName);

        List<Student> FindUnregisteredStudents(GroupName groupName);
    }
}