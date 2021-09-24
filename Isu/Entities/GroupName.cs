using Isu.Tools;

namespace Isu.Entities
{
    public class GroupName
    {
        public GroupName(string groupName)
        {
            if (groupName.Length != 5 || groupName[0] != 'M' || groupName[1] != '3' || groupName[2] < '1' || groupName[2] > '4')
            {
                throw new IsuException("Error: wrong group format.\n");
            }
            else
            {
                CourseNumber = new CourseNumber(groupName[2] - '0');
                GroupNumber = ((groupName[3] - '0') * 10) + groupName[4];
            }
        }

        public CourseNumber CourseNumber
        {
            get;
        }

        public int GroupNumber
        {
            get;
        }
    }
}