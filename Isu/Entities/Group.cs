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
            for (int i = 0; i < ListOfStudents.Count; i++)
            {
                if (ListOfStudents[i].StudentName == studentName)
                {
                    return true;
                }
            }

            return false;
        }

        public Student FindStudent(string studentName)
        {
            for (int i = 0; i < ListOfStudents.Count; i++)
            {
                if (ListOfStudents[i].StudentName == studentName)
                {
                    return ListOfStudents[i];
                }
            }

            return null;
        }
    }
}