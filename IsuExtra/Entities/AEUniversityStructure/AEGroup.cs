using System.Collections.Generic;
using System.Linq;
using IsuExtra.Entities.NamesOfUniversityStructures;
using IsuExtra.Entities.UniversityPeople;

namespace IsuExtra.Entities.AEUniversityStructure
{
    public class AEGroup
    {
        public const int MaximumNumberOfStudents = 25;
        private List<Student> _students;

        public AEGroup(AEGroupName groupName)
        {
            GroupName = groupName;
            _students = new List<Student>();
        }

        public AEGroupName GroupName { get; }

        public List<Student> Students()
        {
            return new List<Student>(_students);
        }

        public Student FindStudent(string studentName)
        {
            return _students.FirstOrDefault(student => student.Name == studentName);
        }

        public Student AddStudent(Student student)
        {
            _students.Add(student);
            return student;
        }

        public Student RemoveStudent(Student student)
        {
            _students.Remove(student);
            return student;
        }

        public List<Student> FindUnregisteredStudents(GroupName groupName)
        {
            return _students.Where(student => Equals(student.GroupName, groupName) && student.AeGroups().Count == 0).ToList();
        }
    }
}