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

        private JobObject(string path, Guid id)
        {
            Path = path;
            Id = id;
        }

        public string Path { get; private set; }
        public Guid Id { get; private set; }

        public string GetInformation()
        {
            return "Job object: { Id: " + Id.ToString() + ", Path to file: " + Path + " }";
        }

        public class Snapshot
        {
            public Snapshot()
            {
            }

            public Snapshot(JobObject jobObject)
            {
                PathSnapshot = jobObject.Path;
                IdSnapshot = jobObject.Id;
            }

            public string PathSnapshot { get; set; }
            public Guid IdSnapshot { get; set; }

            public JobObject Restore()
            {
                return new JobObject(PathSnapshot, IdSnapshot);
            }
        }
    }
}