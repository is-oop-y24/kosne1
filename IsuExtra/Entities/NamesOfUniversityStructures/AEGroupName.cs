using IsuExtra.Tools.SpecificExceptions;

namespace IsuExtra.Entities.NamesOfUniversityStructures
{
    public class AEGroupName
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
    }
}