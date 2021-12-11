using System.Collections.Generic;
using Isu.Entities;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private Faculty _isuService;

        public IsuService()
        {
            _isuService = new Faculty('M');
        }

        public Group AddGroup(GroupName name)
        {
            if (FindGroupBool(name))
            {
                throw new IsuException("Error: Group already exists");
            }

            var group = new Group(name);
            _isuService.AddGroup(group);
            _isuService.AddGroupToCourse(group);
            return group;
        }

        public Student AddStudent(Group group, string name)
        {
            if (FindStudentBool(name))
            {
                throw new IsuException("Error: The student is already in the group");
            }

            GroupName groupName = group.GetGroupName();
            var student = new Student(name, groupName);

            FindGroup(groupName).AddStudent(student);
            FindGroup_InGroups(groupName).AddStudent(student);
            return student;
        }

        public Student GetStudent(int id)
        {
            List<Group> temp = _isuService.GetGroups();
            for (int i = 0; i < temp.Count; i++)
            {
                List<Student> temp1 = temp[i].GetListOfStudents();
                for (int j = 0; j < temp1.Count; j++)
                {
                    if (temp1[j].Id == id)
                    {
                        return temp1[j];
                    }
                }
            }

            throw new IsuException("Error: Student not found");
        }

        public Student FindStudent(string name)
        {
            List<Group> temp = _isuService.GetGroups();
            for (int i = 0; i < temp.Count; i++)
            {
                List<Student> temp1 = temp[i].GetListOfStudents();
                for (int j = 0; j < temp1.Count; j++)
                {
                    if (temp1[j].Name == name)
                    {
                        return temp1[j];
                    }
                }
            }

            throw new IsuException("Error: Student not found");
        }

        public List<Student> FindStudents(GroupName groupName)
        {
            List<Group> temp = _isuService.GetListOfCourses()[(int)groupName.GetCourseNumber()].GetGroups();
            for (int i = 0; i < temp.Count; i++)
            {
                if (groupName == temp[i].GetGroupName())
                {
                    return temp[i].GetListOfStudents();
                }
            }

            throw new IsuException("Error: list of students not found");
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            var listOfStudents = new List<Student>();
            List<Group> temp = _isuService.GetListOfCourses()[(int)courseNumber].GetGroups();
            for (int i = 0; i < temp.Count; i++)
            {
                listOfStudents.AddRange(temp[i].GetListOfStudents());
            }

            return listOfStudents;
        }

        public Group FindGroup(GroupName groupName)
        {
            List<Group> temp = _isuService.GetListOfCourses()[(int)groupName.GetCourseNumber()].GetGroups();
            for (int i = 0; i < temp.Count; i++)
            {
                if (groupName == temp[i].GetGroupName())
                {
                    return temp[i];
                }
            }

            throw new IsuException("Error: Group does not exist");
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            var listGroups = new List<Group>();
            List<Group> temp = _isuService.GetListOfCourses()[(int)courseNumber].GetGroups();
            listGroups.AddRange(temp);

            return listGroups;
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            FindGroupBool(newGroup.GetGroupName());
            _isuService.RemoveStudent(student);
            student.GroupName = newGroup.GetGroupName();
            newGroup.AddStudent(student);
        }

        private bool FindGroupBool(GroupName groupName)
        {
            List<Group> temp = _isuService.GetListOfCourses()[(int)groupName.GetCourseNumber()].GetGroups();
            for (int i = 0; i < temp.Count; i++)
            {
                if (groupName == temp[i].GetGroupName())
                {
                    return true;
                }
            }

            return false;
        }

        private bool FindStudentBool(string name)
        {
            List<Group> temp = _isuService.GetGroups();
            for (int i = 0; i < temp.Count; i++)
            {
                List<Student> temp1 = temp[i].GetListOfStudents();
                for (int j = 0; j < temp1.Count; j++)
                {
                    if (temp1[j].Name == name)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private Group FindGroup_InGroups(GroupName groupName)
        {
            List<Group> temp = _isuService.GetGroups();
            for (int i = 0; i < temp.Count; i++)
            {
                if (groupName == temp[i].GetGroupName())
                {
                    return temp[i];
                }
            }

            throw new IsuException("Error: Group does not exist");
        }
    }
}