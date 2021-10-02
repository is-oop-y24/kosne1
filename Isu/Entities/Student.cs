namespace Isu.Entities
{
    public class Student
    {
        private static int _lastId = -1;
        private Group _studentGroup;
        public Student(string studentName, Group studentGroup)
        {
            StudentName = studentName;
            _studentGroup = studentGroup;
            _lastId++;
            StudentId = _lastId;
        }

        public string StudentName
        {
            get;
        }

        public int StudentId
        {
            get;
        }

        public Group GetStudentGroup()
        {
            return _studentGroup;
        }

        public void ChangeGroup(Group newGroup)
        {
            _studentGroup = newGroup;
        }
    }
}