using System;
using IsuExtra.Entities.GroupInterface;
using IsuExtra.Tools.SpecificExceptions.NameOfUniversityStructuresExceptions;

namespace IsuExtra.Entities.NamesOfUniversityStructures
{
    public class AEGroupName : IGroupNames
    {
        public AEGroupName(string groupName)
        {
            if (groupName.Length != 5 || groupName[3] != '-'
                                      || groupName[4] < '0'
                                      || groupName[4] > '9')
            {
                throw new AEGroupNameException("Error: wrong additional education group format");
            }

            MegaFaculty = groupName[..3];
            Number = groupName[4];
        }

        public string MegaFaculty { get; }
        public int Number { get; }

        public static bool operator ==(AEGroupName groupName1, AEGroupName groupName2)
        {
            if (groupName1 == null)
            {
                throw new AEGroupNameException("Error: null groupName1 value");
            }

            if (groupName2 == null)
            {
                throw new AEGroupNameException("Error: null groupName2 value");
            }

            return (groupName1.Number == groupName2.Number) &&
                   (groupName1.MegaFaculty == groupName2.MegaFaculty);
        }

        public static bool operator !=(AEGroupName groupName1, AEGroupName groupName2)
        {
            if (groupName1 == null)
            {
                throw new AEGroupNameException("Error: null groupName1 value");
            }

            if (groupName2 == null)
            {
                throw new AEGroupNameException("Error: null groupName2 value");
            }

            return (groupName1.Number != groupName2.Number) ||
                   (groupName1.MegaFaculty != groupName2.MegaFaculty);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != GetType())
                return false;

            var other = (AEGroupName)obj;
            return other.ToString() == ToString();
        }

        public bool Equals(AEGroupName other)
        {
            return other != null &&
                   MegaFaculty == other.MegaFaculty &&
                   Number == other.Number;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(MegaFaculty, Number);
        }
    }
}