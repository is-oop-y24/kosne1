using IsuExtra.Entities.Interface;

namespace IsuExtra.Entities.ScheduleStructure
{
    public class GroupSchedule
    {
        public GroupSchedule(IGroupNames groupName)
        {
            GroupName = groupName;
            WeekSchedule = new WeekSchedule();
        }

        public IGroupNames GroupName { get; }
        public WeekSchedule WeekSchedule { get; }
    }
}