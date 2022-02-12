using IsuExtra.Tools.SpecificExceptions;

namespace IsuExtra.Entities.NamesOfUniversityStructures
{
    public class GroupName
    {
        public GroupName(string groupName)
        {
            if (groupName.Length != 5 || groupName[1] != (int)LevelOfEducation.Bachelor
                                      || groupName[1] != (int)LevelOfEducation.Undergraduates
                                      || groupName[2] < '1' || groupName[2] > '4')
            {
                throw new GroupNameException("Error: wrong group format");
            }
            else
            {
                Faculty = groupName[0];
                LevelOfEducation = (LevelOfEducation)groupName[1];
                CourseNumber = (CourseNumber)groupName[2];
                GroupNumber = ((groupName[3] - '0') * 10) + groupName[4] - '0';
            }
        }

        public char Faculty { get; }
        public LevelOfEducation LevelOfEducation { get; }
        public CourseNumber CourseNumber { get; }
        public int GroupNumber { get; }
    }
}