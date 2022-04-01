using System.Collections.Generic;
using Isu.Tools.SpecificExceptions;

namespace Isu.Entities
{
    public class Group
    {
        private const int MaximumNumberOfStudents = 30;
        private List<Student> _listOfStudents;
        private GroupName _groupName;
        private int _numberOfStudents;
        public Group(GroupName groupName)
        {
            _groupName = groupName;
            _listOfStudents = new List<Student>();
        }

        public List<Student> GetListOfStudents()
        {
            return new List<Student>(_listOfStudents);
        }

        public GroupName GetGroupName()
        {
            return _groupName;
        }

        public void AddStudent(Student student)
        {
            _listOfStudents.Add(student);
            ++_numberOfStudents;
            if (_numberOfStudents > MaximumNumberOfStudents)
            {
                throw new GroupException("Error: maximum number of students exceeded");
            }
        }

        public void RemoveStudent(Student student)
        {
            _listOfStudents.Remove(student);
            --_numberOfStudents;
        }
    }
}