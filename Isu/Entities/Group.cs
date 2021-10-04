using System.Collections.Generic;

namespace Isu.Entities
{
    public class Group
    {
        public const int MaxStudents = 30;

        public Group(GroupName name)
        {
            GroupName = name;
            ListOfStudents = new List<Student>();
        }

        public GroupName GroupName
        {
            get;
        }

        public List<Student> ListOfStudents
        {
            get;
        }

        public bool HasStudent(string studentName)
        {
            foreach (Student tempStudent in ListOfStudents)
            {
                if (tempStudent.Name == studentName)
                {
                    return true;
                }
            }

            return false;
        }

        public Student FindStudent(string studentName)
        {
            foreach (Student tempStudent in ListOfStudents)
            {
                if (tempStudent.Name == studentName)
                {
                    return tempStudent;
                }
            }

            return null;
        }
    }
}
