using System;

namespace BackupsExtra.Entities.JobStructure
{
    public class JobObject
    {
        public JobObject(string path)
        {
            Path = path;
            Id = Guid.NewGuid();
        }

        public string Path { get; }
        public Guid Id { get; }
    }
}