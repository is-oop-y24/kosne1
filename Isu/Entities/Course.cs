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
            foreach (Group tempGroup in ListOfGroups)
            {
                if (tempGroup.HasStudent(studentName))
                {
                    return true;
                }
            }

            return false;
        }

        public Student FindStudent(string studentName)
        {
            foreach (Group tempGroup in ListOfGroups)
            {
                if (tempGroup.HasStudent(studentName))
                {
                    return tempGroup.FindStudent(studentName);
                }
            }

            return null;
        }
    }
}
