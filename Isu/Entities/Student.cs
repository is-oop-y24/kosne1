namespace Isu.Entities
{
    public class Student
    {
        private static int _lastId = -1;
        public Student(string studentName, Group studentGroup)
        {
            StudentName = studentName;
            StudentGroup = studentGroup;
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

        public Group StudentGroup
        {
            get;
            set;
        }
    }
}