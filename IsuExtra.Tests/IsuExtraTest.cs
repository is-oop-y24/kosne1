using System;
using System.Collections.Generic;
using System.Linq;
using IsuExtra.Entities.AEUniversityStructure;
using IsuExtra.Entities.NamesOfUniversityStructures;
using IsuExtra.Entities.ScheduleStructure;
using IsuExtra.Entities.UniversityFacilities;
using IsuExtra.Entities.UniversityPeople;
using IsuExtra.Entities.UniversityStructure;
using IsuExtra.Services.AEUniversityService;
using IsuExtra.Services.ScheduleStructureService;
using IsuExtra.Services.UniversityStructureService;
using IsuExtra.Tools.SpecificExceptions.AEExceptions;
using IsuExtra.Tools.SpecificExceptions.UniversityPeopleException;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    public class Tests
    {
        private IUniversityManager universityManager;
        private IAEUniversityManager aeUniversityManager;
        private IScheduleManager scheduleManager;

        [SetUp]
        public void Setup()
        {
            universityManager = new UniversityManager();
            aeUniversityManager = new AEUniversityManager();
            scheduleManager = new ScheduleManager();
        }

        [Test]
        public void AddStudentToNewAEGroup_ScheduleNotIntersect_Success()
        {
            var groupName = new GroupName("M3207");
            Faculty faculty = universityManager.AddFaculty(groupName.Faculty);
            Course course = universityManager.AddCourse(faculty, groupName.CourseNumber);
            Group group = universityManager.AddGroup(groupName);

            var teacher1 = new Teacher("Шоев Владислав Иванович");
            var auditorium1 = new Auditorium(545, CampusAddress.Address2);

            var teacher2 = new Teacher("Полозков Роман Григорьевич");
            var auditorium2 = new Auditorium(550, CampusAddress.Address2);
            
            scheduleManager.AddGroupSchedule(groupName);
            scheduleManager.AddDaySchedule(DayOfWeek.Monday, groupName);
            Lesson lesson1 = scheduleManager.AddLesson(DayOfWeek.Monday, LessonBeginning.Lesson3, teacher1, groupName, auditorium1);
            Lesson lesson2 = scheduleManager.AddLesson(DayOfWeek.Monday, LessonBeginning.Lesson5, teacher2, groupName, auditorium2);

            AECourse aeCourse = aeUniversityManager.AddCourse("СИА", 'N');
            var aeGroupName = new AEGroupName("СИА-3");
            AEGroup aeGroup = aeUniversityManager.AddGroup(aeGroupName);
            
            var teacher3 = new Teacher("Береснев Артем Дмитриевич");
            var auditorium3 = new Auditorium(500, CampusAddress.Address2);
            
            scheduleManager.AddGroupSchedule(aeGroupName);
            DaySchedule day1 = scheduleManager.AddDaySchedule(DayOfWeek.Monday, aeGroupName);
            scheduleManager.AddLesson(DayOfWeek.Monday, LessonBeginning.Lesson4, teacher3, aeGroupName, auditorium3);
            
            Student student = universityManager.AddStudent(group, "Ананьин Николай");
            universityManager.AddStudent(group, student.Name);
            
            if (scheduleManager.ScheduleIntersect(groupName, aeGroupName))
            {
                Assert.Fail();
            }

            aeUniversityManager.AddStudent(aeGroup, student);
        }

        [Test]
        public void AddStudentToNewAEGroup_ScheduleIntersect()
        {
            var groupName = new GroupName("M3207");
            Faculty faculty = universityManager.AddFaculty(groupName.Faculty);
            Course course = universityManager.AddCourse(faculty, groupName.CourseNumber);
            Group group = universityManager.AddGroup(groupName);

            var teacher1 = new Teacher("Шоев Владислав Иванович");
            var auditorium1 = new Auditorium(545, CampusAddress.Address2);

            var teacher2 = new Teacher("Полозков Роман Григорьевич");
            var auditorium2 = new Auditorium(550, CampusAddress.Address2);
            
            scheduleManager.AddGroupSchedule(groupName);
            scheduleManager.AddDaySchedule(DayOfWeek.Monday, groupName);
            Lesson lesson1 = scheduleManager.AddLesson(DayOfWeek.Monday, LessonBeginning.Lesson3, teacher1, groupName, auditorium1);
            Lesson lesson2 = scheduleManager.AddLesson(DayOfWeek.Monday, LessonBeginning.Lesson5, teacher2, groupName, auditorium2);

            AECourse aeCourse = aeUniversityManager.AddCourse("СИА", 'N');
            var aeGroupName = new AEGroupName("СИА-3");
            AEGroup aeGroup = aeUniversityManager.AddGroup(aeGroupName);
            
            var teacher3 = new Teacher("Береснев Артем Дмитриевич");
            var auditorium3 = new Auditorium(500, CampusAddress.Address2);
            
            scheduleManager.AddGroupSchedule(aeGroupName);
            DaySchedule day1 = scheduleManager.AddDaySchedule(DayOfWeek.Monday, aeGroupName);
            scheduleManager.AddLesson(DayOfWeek.Monday, LessonBeginning.Lesson3, teacher3, aeGroupName, auditorium3);
            
            Student student = universityManager.AddStudent(group, "Ананьин Николай");
            universityManager.AddStudent(group, student.Name);
            
            if (!scheduleManager.ScheduleIntersect(groupName, aeGroupName))
            {
                Assert.Fail();
            }
        }
        
        [Test]
        public void AddStudentToNewAEGroup_RemoveStudent_Success()
        {
            var groupName = new GroupName("M3207");
            Faculty faculty = universityManager.AddFaculty(groupName.Faculty);
            Course course = universityManager.AddCourse(faculty, groupName.CourseNumber);
            Group group = universityManager.AddGroup(groupName);

            var teacher1 = new Teacher("Шоев Владислав Иванович");
            var auditorium1 = new Auditorium(545, CampusAddress.Address2);

            var teacher2 = new Teacher("Полозков Роман Григорьевич");
            var auditorium2 = new Auditorium(550, CampusAddress.Address2);
            
            scheduleManager.AddGroupSchedule(groupName);
            scheduleManager.AddDaySchedule(DayOfWeek.Monday, groupName);
            Lesson lesson1 = scheduleManager.AddLesson(DayOfWeek.Monday, LessonBeginning.Lesson3, teacher1, groupName, auditorium1);
            Lesson lesson2 = scheduleManager.AddLesson(DayOfWeek.Monday, LessonBeginning.Lesson5, teacher2, groupName, auditorium2);

            AECourse aeCourse = aeUniversityManager.AddCourse("СИА", 'N');
            var aeGroupName = new AEGroupName("СИА-3");
            AEGroup aeGroup = aeUniversityManager.AddGroup(aeGroupName);
            
            var teacher3 = new Teacher("Береснев Артем Дмитриевич");
            var auditorium3 = new Auditorium(500, CampusAddress.Address2);
            
            scheduleManager.AddGroupSchedule(aeGroupName);
            DaySchedule day1 = scheduleManager.AddDaySchedule(DayOfWeek.Monday, aeGroupName);
            scheduleManager.AddLesson(DayOfWeek.Monday, LessonBeginning.Lesson4, teacher3, aeGroupName, auditorium3);
            
            Student student = universityManager.AddStudent(group, "Ананьин Николай");
            universityManager.AddStudent(group, student.Name);
            
            if (scheduleManager.ScheduleIntersect(groupName, aeGroupName))
            {
                Assert.Fail();
            }

            aeUniversityManager.AddStudent(aeGroup, student);

            aeUniversityManager.RemoveStudent(aeGroup, student.Name);
            
            if (student.AeGroups().Any(groupName => Equals(groupName, aeGroupName)) || aeGroup.FindStudent(student.Name) != default)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void AddStudentToNewAEGroup_RemoveStudent_ThrowException()
        {
            var groupName = new GroupName("M3207");
            Faculty faculty = universityManager.AddFaculty(groupName.Faculty);
            Course course = universityManager.AddCourse(faculty, groupName.CourseNumber);
            Group group = universityManager.AddGroup(groupName);

            var teacher1 = new Teacher("Шоев Владислав Иванович");
            var auditorium1 = new Auditorium(545, CampusAddress.Address2);

            var teacher2 = new Teacher("Полозков Роман Григорьевич");
            var auditorium2 = new Auditorium(550, CampusAddress.Address2);
            
            scheduleManager.AddGroupSchedule(groupName);
            scheduleManager.AddDaySchedule(DayOfWeek.Monday, groupName);
            Lesson lesson1 = scheduleManager.AddLesson(DayOfWeek.Monday, LessonBeginning.Lesson3, teacher1, groupName, auditorium1);
            Lesson lesson2 = scheduleManager.AddLesson(DayOfWeek.Monday, LessonBeginning.Lesson5, teacher2, groupName, auditorium2);

            AECourse aeCourse = aeUniversityManager.AddCourse("СИА", 'N');
            var aeGroupName = new AEGroupName("СИА-3");
            AEGroup aeGroup = aeUniversityManager.AddGroup(aeGroupName);
            
            var teacher3 = new Teacher("Береснев Артем Дмитриевич");
            var auditorium3 = new Auditorium(500, CampusAddress.Address2);
            
            scheduleManager.AddGroupSchedule(aeGroupName);
            DaySchedule day1 = scheduleManager.AddDaySchedule(DayOfWeek.Monday, aeGroupName);
            scheduleManager.AddLesson(DayOfWeek.Monday, LessonBeginning.Lesson4, teacher3, aeGroupName, auditorium3);
            
            Student student = universityManager.AddStudent(group, "Ананьин Николай");
            universityManager.AddStudent(group, student.Name);

            Assert.Catch<AEGroupException>(() =>
            {
                aeUniversityManager.RemoveStudent(aeGroup, student.Name);
            });
        }

        [Test]
        public void FindUnregisteredStudents_Success()
        {
            var groupName = new GroupName("M3207");
            Faculty faculty = universityManager.AddFaculty(groupName.Faculty);
            Course course = universityManager.AddCourse(faculty, groupName.CourseNumber);
            Group group = universityManager.AddGroup(groupName);

            var teacher1 = new Teacher("Шоев Владислав Иванович");
            var auditorium1 = new Auditorium(545, CampusAddress.Address2);

            var teacher2 = new Teacher("Полозков Роман Григорьевич");
            var auditorium2 = new Auditorium(550, CampusAddress.Address2);
            
            scheduleManager.AddGroupSchedule(groupName);
            scheduleManager.AddDaySchedule(DayOfWeek.Monday, groupName);
            Lesson lesson1 = scheduleManager.AddLesson(DayOfWeek.Monday, LessonBeginning.Lesson3, teacher1, groupName, auditorium1);
            Lesson lesson2 = scheduleManager.AddLesson(DayOfWeek.Monday, LessonBeginning.Lesson5, teacher2, groupName, auditorium2);
            
            for (int i = 0; i < 20; i++)
            {
                Student student = universityManager.AddStudent(group, i.ToString());
            }
            Student student1 = universityManager.AddStudent(group, "Ананьин Николай");

            AECourse aeCourse = aeUniversityManager.AddCourse("СИА", 'N');
            var aeGroupName = new AEGroupName("СИА-3");
            AEGroup aeGroup = aeUniversityManager.AddGroup(aeGroupName);
            
            var teacher3 = new Teacher("Береснев Артем Дмитриевич");
            var auditorium3 = new Auditorium(500, CampusAddress.Address2);
            
            scheduleManager.AddGroupSchedule(aeGroupName);
            DaySchedule day1 = scheduleManager.AddDaySchedule(DayOfWeek.Monday, aeGroupName);
            scheduleManager.AddLesson(DayOfWeek.Monday, LessonBeginning.Lesson4, teacher3, aeGroupName, auditorium3);
            
            universityManager.AddStudent(group, student1.Name);
            
            if (scheduleManager.ScheduleIntersect(groupName, aeGroupName))
            {
                Assert.Fail();
            }

            aeUniversityManager.AddStudent(aeGroup, student1);

            List<Student> students = aeUniversityManager.FindUnregisteredStudents(groupName);
            
            if (students.Any(student => student.Id == student1.Id))
            {
                Assert.Fail();
            }
        }

        [Test]
        public void AddStudentToHisFacultyAEGroup_ThrowException()
        {
            var groupName = new GroupName("M3207");
            Faculty faculty = universityManager.AddFaculty(groupName.Faculty);
            Course course = universityManager.AddCourse(faculty, groupName.CourseNumber);
            Group group = universityManager.AddGroup(groupName);
            
            var teacher1 = new Teacher("Шоев Владислав Иванович");
            var auditorium1 = new Auditorium(545, CampusAddress.Address2);

            var teacher2 = new Teacher("Полозков Роман Григорьевич");
            var auditorium2 = new Auditorium(550, CampusAddress.Address2);
            
            scheduleManager.AddGroupSchedule(groupName);
            scheduleManager.AddDaySchedule(DayOfWeek.Monday, groupName);
            Lesson lesson1 = scheduleManager.AddLesson(DayOfWeek.Monday, LessonBeginning.Lesson3, teacher1, groupName, auditorium1);
            Lesson lesson2 = scheduleManager.AddLesson(DayOfWeek.Monday, LessonBeginning.Lesson5, teacher2, groupName, auditorium2);
            
            Student student = universityManager.AddStudent(group, "Ананьин Николай");
            
            AECourse aeCourse = aeUniversityManager.AddCourse("СИА", 'M');
            var aeGroupName = new AEGroupName("СИА-3");
            AEGroup aeGroup = aeUniversityManager.AddGroup(aeGroupName);
            
            var teacher3 = new Teacher("Береснев Артем Дмитриевич");
            var auditorium3 = new Auditorium(500, CampusAddress.Address2);
            
            scheduleManager.AddGroupSchedule(aeGroupName);
            DaySchedule day1 = scheduleManager.AddDaySchedule(DayOfWeek.Monday, aeGroupName);
            scheduleManager.AddLesson(DayOfWeek.Monday, LessonBeginning.Lesson4, teacher3, aeGroupName, auditorium3);
            
            universityManager.AddStudent(group, student.Name);
            
            Assert.Catch<StudentException>(() =>
            {
                aeUniversityManager.AddStudent(aeGroup, student);
            });
        }
    }
}