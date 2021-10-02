using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Entities
{
    public class CourseNumber
    {
        public const int MaxCourseNumber = 4;

        public CourseNumber(int courseNumber)
        {
            if (courseNumber < 1 || courseNumber > MaxCourseNumber)
            {
                throw new IsuException("Wrong course number.\n");
            }

            Number = courseNumber;
        }

        public int Number
        {
            get;
            set;
        }
    }
}