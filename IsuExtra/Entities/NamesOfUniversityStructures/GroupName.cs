using System;
using IsuExtra.Entities.Interface;
using IsuExtra.Tools.SpecificExceptions;

namespace IsuExtra.Entities.NamesOfUniversityStructures
{
    public class GroupName : IGroupNames
    {
        public GroupName(string groupName)
        {
            if (groupName.Length != 5 || groupName[1] < '3'
                                      || groupName[1] > '5'
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

        public static bool operator ==(GroupName groupName1, GroupName groupName2)
        {
            if (groupName1 == null)
            {
                throw new GroupNameException("Error: null groupName1 value");
            }

            if (groupName2 == null)
            {
                throw new GroupNameException("Error: null groupName2 value");
            }

            return (groupName1.Faculty == groupName2.Faculty) &&
                   ((int)groupName1.LevelOfEducation == (int)groupName2.LevelOfEducation) &&
                   ((int)groupName1.CourseNumber == (int)groupName2.CourseNumber) &&
                   (groupName1.GroupNumber == groupName2.GroupNumber);
        }

        public static bool operator !=(GroupName groupName1, GroupName groupName2)
        {
            if (groupName1 == null)
            {
                throw new GroupNameException("Error: null groupName1 value");
            }

            if (groupName2 == null)
            {
                throw new GroupNameException("Error: null groupName2 value");
            }

            return (groupName1.Faculty != groupName2.Faculty) ||
                   ((int)groupName1.LevelOfEducation != (int)groupName2.LevelOfEducation) ||
                   ((int)groupName1.CourseNumber != (int)groupName2.CourseNumber) ||
                   (groupName1.GroupNumber != groupName2.GroupNumber);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as GroupName);
        }

        public bool Equals(GroupName other)
        {
            return other != null &&
                   Faculty == other.Faculty &&
                   LevelOfEducation == other.LevelOfEducation &&
                   CourseNumber == other.CourseNumber &&
                   GroupNumber == other.GroupNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Faculty, LevelOfEducation, CourseNumber, GroupNumber);
        }
    }
}