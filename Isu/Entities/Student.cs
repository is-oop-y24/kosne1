namespace Isu.Entities
{
    public class Student
    {
        private static int _lastId = -1;
        public Student(string studentName, GroupName groupName)
        {
            Name = studentName;
            GroupName = groupName;
            _lastId++;
            Id = _lastId;
        }

        public Student(string studentName, GroupName groupName, int id)
        {
            Name = studentName;
            GroupName = groupName;
            Id = id;
        }

        public string Name
        {
            get;
        }

        public int Id
        {
            get;
        }

        public GroupName GroupName
        {
            get;
            set;
        }
    }
}
