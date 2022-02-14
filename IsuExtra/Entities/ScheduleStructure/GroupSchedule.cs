using IsuExtra.Entities.NamesOfUniversityStructures;

namespace IsuExtra.Entities.ScheduleStructure
{
    public class GroupSchedule
    {
        public GroupSchedule(GroupName groupName)
        {
            GroupName = groupName;
            WeekSchedule = new WeekSchedule();
        }

        public GroupName GroupName { get; }
        public WeekSchedule WeekSchedule { get; }
    }
}