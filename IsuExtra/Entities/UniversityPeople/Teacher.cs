using System;

namespace IsuExtra.Entities.UniversityPeople
{
    public class Teacher
    {
        public Teacher(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public Guid Id { get; }
        public string Name { get; }
    }
}