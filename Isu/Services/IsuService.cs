using System.Collections.Generic;
using Isu.Entities;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private List<Course> _fitip;
        public IsuService()
        {
            _fitip = new List<Course>();
            for (int i = 1; i <= CourseNumber.MaxCourseNumber; i++)
            {
                _fitip.Add(new Course(new CourseNumber(i)));
            }
        }

        public Group AddGroup(GroupName name)
        {
            if (HasGroup(name))
            {
                throw new IsuException("A group with the same name already exists.\n");
            }
            else
            {
                var group = new Group(name);
                _fitip[name.CourseNumber.Number].ListOfGroups.Add(group);
                return group;
            }
        }

        public Student AddStudent(Group group, string name)
        {
            Student student = FindStudent(name);
            if (group.ListOfStudents.Count > Group.MaxStudents)
            {
                throw new IsuException("exceeding the number of people in the group.\n");
            }

            if (student == null)
            {
                student = new Student(name, group.GroupName);
                group.ListOfStudents.Add(student);
            }
            else
            {
                if (student.GroupName == group.GroupName)
                {
                    throw new IsuException("the student is already in this group.\n");
                }
                else
                {
                    throw new IsuException("the student is already in another group.\n");
                }
            }

            return student;
        }

        public Student GetStudent(int id)
        {
            foreach (Course tempCourse in _fitip)
            {
                foreach (Group tempGroup in tempCourse.ListOfGroups)
                {
                    foreach (Student tempStudent in tempGroup.ListOfStudents)
                    {
                        if (tempStudent.Id == id)
                        {
                            return tempStudent;
                        }
                    }
                }
            }

            return null;
        }

        public Student FindStudent(string name)
        {
            foreach (Course tempCourse in _fitip)
            {
                if (tempCourse.HasStudent(name))
                {
                    return tempCourse.FindStudent(name);
                }
            }

            return null;
        }

        public bool HasStudent(string name)
        {
            foreach (Course tempCourse in _fitip)
            {
                if (tempCourse.HasStudent(name))
                {
                    return true;
                }
            }

            return false;
        }

        public List<Student> FindStudents(GroupName groupName)
        {
            return FindGroup(groupName).ListOfStudents;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            var listOfstudents = new List<Student>();
            foreach (Group tempGroup in _fitip[courseNumber.Number].ListOfGroups)
            {
                foreach (Student tempStudent in tempGroup.ListOfStudents)
                {
                    listOfstudents.Add(tempStudent);
                }
            }

            return listOfstudents;
        }

        public Group FindGroup(GroupName groupName)
        {
            foreach (Group tempGroup in _fitip[groupName.CourseNumber.Number - 1].ListOfGroups)
            {
                if (tempGroup.GroupName == groupName)
                {
                    var output = new Group(groupName);
                    output = tempGroup;
                    return output;
                }
            }

            return null;
        }

        public bool HasGroup(GroupName groupName)
        {
            foreach (Group tempGroup in _fitip[groupName.CourseNumber.Number - 1].ListOfGroups)
            {
                if (tempGroup.GroupName == groupName)
                {
                    return true;
                }
            }

            return false;
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            List<Group> output = _fitip[courseNumber.Number].ListOfGroups;
            return output;
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            if (newGroup.ListOfStudents.Count < Group.MaxStudents)
            {
                for (int i = 0; i < _fitip.Count; i++)
                {
                    for (int j = 0; j < _fitip[i].ListOfGroups.Count; j++)
                    {
                        if (_fitip[i].ListOfGroups[j].GroupName == student.GroupName)
                        {
                            _fitip[i].ListOfGroups[j].ListOfStudents.Remove(student);
                        }
                    }
                }

                student.GroupName = newGroup.GroupName;
                newGroup.ListOfStudents.Add(student);
            }
            else
            {
                throw new IsuException("transfer is not possible");
            }
        }
    }
}
