using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Isu.Entities
{
    public class Course
    {
        public Course(CourseNumber courseNumber)
        {
            CourseNumber = courseNumber;
            ListOfGroups = new List<Group>();
        }

        public CourseNumber CourseNumber
        {
            get;
        }

        public List<Group> ListOfGroups
        {
            get;
        }

        public bool HasStudent(string studentName)
        {
            for (int i = 0; i < ListOfGroups.Count; i++)
            {
                if (ListOfGroups[i].HasStudent(studentName))
                {
                    return true;
                }
            }

            return false;
        }

        public Student FindStudent(string studentName)
        {
            for (int i = 0; i < ListOfGroups.Count; i++)
            {
                if (ListOfGroups[i].HasStudent(studentName))
                {
                    return ListOfGroups[i].FindStudent(studentName);
                }
            }

            return null;
        }
    }
}