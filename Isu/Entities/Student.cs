namespace Isu.Entities
{
    public class Student
    {
        private static int _lastId = -1;
        private Group _studentGroup;
        public Student(string studentName, Group studentGroup)
        {
            Name = studentName;
            _studentGroup = studentGroup;
            _lastId++;
            Id = _lastId;
        }

        public Student(string studentName, Group studentGroup, int id)
        {
            Name = studentName;
            _studentGroup = studentGroup;
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

        public Group GetStudentGroup()
        {
            return _studentGroup;
        }
    }
}
