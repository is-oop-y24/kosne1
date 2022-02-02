using System.Collections.Generic;

namespace Isu.Entities
{
    public class Faculty
    {
        private char _name;
        private List<Course> _listOfCourses;
        private List<Group> _groups;
        public Faculty(char facultyName)
        {
            _name = facultyName;
            _groups = new List<Group>();
            _listOfCourses = new List<Course>();
            for (int i = 1; i <= 4; i++)
            {
                var tempCourse = new Course(i);
                _listOfCourses.Add(tempCourse);
            }
        }

        public List<Group> GetGroups()
        {
            return new List<Group>(_groups);
        }

        public List<Course> GetListOfCourses()
        {
            return new List<Course>(_listOfCourses);
        }

        public Group AddGroup(Group group)
        {
            _groups.Add(group);
            return group;
        }

        public void AddGroupToCourse(Group group)
        {
            _listOfCourses[(int)group.GetGroupName().GetCourseNumber()].AddGroup(group);
        }

        public void RemoveStudent(Student student)
        {
            _listOfCourses[(int)student.GroupName.GetCourseNumber()].RemoveStudent(student);
            for (int i = 0; i < _groups.Count; i++)
            {
                if (student.GroupName == _groups[i].GetGroupName())
                {
                    _groups[i].RemoveStudent(student);
                }
            }
        }
    }
}