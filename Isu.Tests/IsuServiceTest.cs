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
            _isuService = new IsuService();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group group = _isuService.AddGroup(new GroupName("M3207"));
            _isuService.AddStudent(group, "Ananin Nikolai");
            Assert.Catch<IsuException>(() =>
            {
                _isuService.AddStudent(group, "Ananin Nikolai");
            });
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Group group = _isuService.AddGroup(new GroupName("M3207"));
            Assert.Catch<IsuException>(() =>
            {
                for (int i = 0; i < 31; i++)
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
            Group newGroup = _isuService.AddGroup(new GroupName("M3202"));
            Student student = _isuService.AddStudent(group, "Ananin Nikolai");
            _isuService.ChangeStudentGroup(student, newGroup);
            if (_isuService.FindStudent(student.Name).GroupName != newGroup.GetGroupName()
                || student.GroupName == group.GetGroupName())
            {
                Assert.Fail();    
            }
        }
    }
}