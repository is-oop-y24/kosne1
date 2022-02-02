using Isu.Tools.SpecificExceptions;

namespace Isu.Entities
{
    public class GroupName
    {
        private readonly int _number;
        private readonly CourseNumber _courseNumber;
        private readonly char _faculty;
        public GroupName(string name)
        {
            if (name.Length != 5 || name[0] != 'M' || name[1] != '3' || name[2] < '1' || name[2] > '4')
            {
                throw new GroupException("Error: wrong group format.\n");
            }
            else
            {
                _number = ((name[3] - '0') * 10) + name[4] - '0';
                _courseNumber = (CourseNumber)name[2] - '0';
                _faculty = name[0];
            }
        }

        public int GetNumber()
        {
            return _number;
        }

        public CourseNumber GetCourseNumber()
        {
            return _courseNumber;
        }

        public char GetFaculty()
        {
            return _faculty;
        }
    }
}