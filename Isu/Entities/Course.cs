using System.Collections.Generic;

namespace Isu.Entities
{
    public class Course
    {
        private KeyValuePair<CourseNumber, List<Group>> _course;

        public Course(int courseNumber)
        {
            _course = new KeyValuePair<CourseNumber, List<Group>>((CourseNumber)courseNumber, new List<Group>());
        }

        public List<Group> GetGroups()
        {
            return new List<Group>(_course.Value);
        }

        public void AddGroup(Group group)
        {
            _course.Value.Add(group);
        }

        public void RemoveStudent(Student student)
        {
            for (int i = 0; i < _course.Value.Count; i++)
            {
                if (student.GroupName == _course.Value[i].GetGroupName())
                {
                    _course.Value[i].RemoveStudent(student);
                }
            }
        }
    }
}