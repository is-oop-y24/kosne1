namespace Isu.Entities
{
    public class Student
    {
        private static int _lastId = -1;

        public Student(string name, GroupName groupName)
        {
            ++_lastId;
            Id = _lastId;
            Name = name;
            GroupName = groupName;
        }

        public int Id { get; }
        public string Name { get; }
        public GroupName GroupName { get; set; }
    }
}