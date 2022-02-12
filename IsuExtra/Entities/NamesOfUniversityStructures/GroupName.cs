using IsuExtra.Tools.SpecificExceptions;

namespace IsuExtra.Entities.NamesOfUniversityStructures
{
    public class GroupName
    {
        public GroupName(string groupName)
        {
            if (groupName.Length != 5 || groupName[1] != (int)Education.Bachelor
                                      || groupName[1] != (int)Education.Undergraduates
                                      || groupName[2] < '1' || groupName[2] > '4')
            {
                throw new GroupNameException("Error: wrong group format");
            }
            else
            {
                Faculty = groupName[0];
                Education = (Education)groupName[1];
                CourseNumber = (CourseNumber)groupName[2];
                GroupNumber = ((groupName[3] - '0') * 10) + groupName[4] - '0';
            }
        }

        public char Faculty { get; }
        public Education Education { get; }
        public CourseNumber CourseNumber { get; }
        public int GroupNumber { get; }
    }
}