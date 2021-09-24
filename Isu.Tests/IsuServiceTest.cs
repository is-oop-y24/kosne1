using System;
using Isu.Entities;
using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            // TODO: implement
            _isuService = new IsuService();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group group = _isuService.AddGroup(new GroupName("M3207"));
            Student student = _isuService.AddStudent(group, "pidoras");
            if (group.HasStudent("pidoras") && student.StudentGroup == group)
            {
                Console.WriteLine("OKAY");
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group = _isuService.AddGroup(new GroupName("M3207"));
                for (int i = 0; i < 40; i++)
                {
                    _isuService.AddStudent(group, i.ToString());
                }
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                _isuService.AddGroup(new GroupName("N3207"));
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Group group = _isuService.AddGroup(new GroupName("M3207"));
            Student student = _isuService.AddStudent(group, "pidoras");
            Group newGroup = _isuService.AddGroup(new GroupName("M3202"));
            _isuService.ChangeStudentGroup(student, newGroup);
            if (!group.HasStudent("pidoras") && newGroup.HasStudent("pidoras"))
            {
                Console.WriteLine("OKAY");
            }
            else
            {
                throw new IsuException("pidors nagovnokodil");
            }
        }
    }
}